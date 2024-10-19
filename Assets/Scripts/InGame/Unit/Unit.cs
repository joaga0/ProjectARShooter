using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.HableCurve;

public abstract class Unit : MonoBehaviour
{
    #region Enum
    protected enum State
    {
        None,
        Init,
        Idle,
        Move,
        Attack,
        Death,
        Destory,
        SkillCast,
        CC
    }
    protected enum UnitType
    {
        MeleeCreature,
        RangedCreature,
        Boss,
        Tower
    }
    #endregion

    #region Serialize Field
    [Header("Unit Info")]
    [SerializeField] protected int team;
    [SerializeField] protected UnitType unitType;
    [SerializeField] protected int tier;
    [Header("Init Value")]
    [SerializeField] protected int initHp;
    [SerializeField] private float _initSpeed;
    [SerializeField] protected int initAtkPwr;
    [SerializeField] protected float initAtkCoolTime;
    [SerializeField] private float _initAtkRng;
    [SerializeField] private float _initSrchRng;
    [Header("ETC")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    #endregion

    #region Protected Field
    protected float initSpeed;
    protected float initAtkRng;
    protected float initSrchRng;

    protected int maxHp;
    protected int curHp;
    protected int curShield;
    protected float curSpeed;
    protected int curAtkPwr;
    protected float curAtkCoolTime;
    protected float curAtkRng;
    protected float curSrchRng;

    private State _nowState;
    private State _nextState;

    protected Unit targetObj;

    protected bool isAtkCoolTime;
    #endregion

    #region Get/Set
    protected State nowState { 
        get { return _nowState; }
        set { _nowState = value; }
    }
    protected State nextState {
        get { return _nextState; }
        set {
            if(nowState != value)
            {
                _nextState = value;
                StateUpdate();
            }          
        }
    }

    public Unit TargetObj => targetObj;
    public int Team => team;
    #endregion

    #region Abstract Method
    protected abstract void StateUpdate();
    #endregion

    #region Protected Method
    protected void AtkCoolTime()
    {
        StartCoroutine(AtkCoolTimeCoroutine());
    }
    #endregion

    #region Public Method
    public void GetDamage(int dmg)
    {
        if(curShield > 0)
        {
            if(dmg > curShield)
            {
                //Shield Break Effect
                curShield = 0;
                curHp -= (dmg - curShield); 
            }
            else
            {
                //Shield Hit Effect
                curShield -= dmg;
            }
        }
        else
        {
            //Hit Effect
            curHp -= dmg;
        }

        if(curHp < 0)
        {
            nextState = State.Death;
        }
    }
    #endregion

    #region Private Method
    protected void Init()
    {
        if(animator==null) animator = GetComponent<Animator>();

        initSpeed = _initSpeed / 100; //m to cm
        initAtkRng = _initAtkRng / 100;
        initSrchRng = _initSrchRng / 100;

        maxHp = initHp;
        curHp = maxHp;
        curShield = 0;

        curSpeed = initSpeed;
        curAtkPwr = initAtkPwr;
        curAtkCoolTime = initAtkCoolTime;
        curAtkRng = initAtkRng;

        curSrchRng = initSrchRng;

        isAtkCoolTime = false;
        nextState = State.Idle;
    }
    private IEnumerator AtkCoolTimeCoroutine()
    {
        isAtkCoolTime = true;
        yield return new WaitForSeconds(curAtkCoolTime);
        isAtkCoolTime = false;
    }
    #endregion

    #region Debug_Gizmo
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform.position;
        float angle = 0f;
        float angleStep = 360f / 60;
        Vector3 prevPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _initAtkRng / 100, 0, Mathf.Sin(Mathf.Deg2Rad * angle) * _initAtkRng / 100);
        for (int i = 1; i <= 60; i++)
        {
            angle += angleStep;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _initAtkRng / 100, 0, Mathf.Sin(Mathf.Deg2Rad * angle) * _initAtkRng / 100);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }

        Gizmos.color = Color.blue;
        position = transform.position;
        angle = 0f;
        angleStep = 360f / 60;
        prevPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _initSrchRng / 100, 0, Mathf.Sin(Mathf.Deg2Rad * angle) * _initSrchRng / 100);
        for (int i = 1; i <= 60; i++)
        {
            angle += angleStep;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _initSrchRng / 100, 0, Mathf.Sin(Mathf.Deg2Rad * angle) * _initSrchRng / 100);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

    #endregion
}
