using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Attack Info")]
    public float attackRange = 100f;    //공격범위
    public int attackDamage = 20;       //공격 데미지
    [SerializeField] private float attackCooldown = 10f;
    public Camera playerCamera;
    public Button attackButton;
    private bool canAttack = true;

    [Header("Obj")]
    public Animator crossbowAnimator;   //석궁 Animator
    //public GameObject projectilePrefab; //발사체
    //public Transform firePoint;         //발사 위치
    public GameObject arrow;        //화살
    public ArrowController ac;

    private enum Mode {Attack, Item}    //모드 변경
    private Mode currentMode = Mode.Item;
    public Button modeChangeButton;
    public GameObject itemInventoryUI;

    [Header("pos")]
    public Transform crossbow;
    public Transform attackModePos;   //공격 모드 석궁 회전
    public Transform itemModePos;     //아이템 모드 석궁 회전


    void Start()
    {
        attackButton.onClick.AddListener(Attack);
        modeChangeButton.onClick.AddListener(ChangeMode);
        SetItemMode();
        PlayAnimationToEnd();
    }

    void Update()
    {
        
    }

    private void ChangeMode()   //모드 변경
    {
        if(canAttack){
            if (currentMode == Mode.Attack)
            {
                SetItemMode();
            }
            else
            {
                SetAttackMode();
            }
        }
    }

    private void SetAttackMode()    //공격 모드로 변경
    {
        currentMode = Mode.Attack;
        attackButton.gameObject.SetActive(true);
        itemInventoryUI.SetActive(false);
        arrow.SetActive(true);
        if (crossbowAnimator != null)
        {
            crossbowAnimator.SetTrigger("Stop");
        }

        if (crossbow != null)
        {
            crossbow.position = attackModePos.position;
            crossbow.rotation = attackModePos.rotation;
        }
    }

    private void SetItemMode()      //아이템 모드로 변경
    {
        currentMode = Mode.Item;
        attackButton.gameObject.SetActive(false);
        itemInventoryUI.SetActive(true);
        arrow.SetActive(false);
        if (crossbowAnimator != null)
        {
            //crossbowAnimator.SetTrigger("Idle");
        }

        if (crossbow != null)
        {
            crossbow.position = itemModePos.position;
            crossbow.rotation = itemModePos.rotation;
        }
    }

    //공격
    void Attack()
    {
        if(canAttack)
        {
            //석궁 애니메이션 재생
            if (crossbowAnimator != null)
            {
                crossbowAnimator.SetTrigger("Shooting");
                crossbowAnimator.SetTrigger("Stop");
            }

            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, attackRange))
            {
                Monster monster = hit.transform.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.Damage(attackDamage);
                }
            } 
            ac.ShowArrow(false);
            StartCoroutine(AttackDelay());
        }
    }

        void PlayAnimationToEnd()
    {
        if (crossbowAnimator != null)
        {
            AnimatorStateInfo stateInfo = crossbowAnimator.GetCurrentAnimatorStateInfo(0);
            crossbowAnimator.Play(stateInfo.fullPathHash, 0, 1f); // 1f는 애니메이션의 마지막 프레임
        }
    }


    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        ac.ShowArrow(true);
    }
}
