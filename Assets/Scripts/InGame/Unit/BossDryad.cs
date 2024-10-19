using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDryad : Unit
{
    #region SerializeField
    [Header("Boss - 1 Phase")]
    [SerializeField] float healCoolTime;
    [SerializeField] int healValue;
    [SerializeField] float healRange;
    [Header("Boss - 2 Phase")]
    [SerializeField] float phaseTwoTriggerHp;
    [SerializeField] float phaseTwoSpeed;
    [SerializeField] int phaseTwoAtkPwr;
    [SerializeField] float phaseTwoAtkCoolTime;
    [SerializeField] float phaseTwoAtkRng;
    [SerializeField] float phaseTwoSrchRng;
    #endregion

    #region private method
    bool isTwoPhase;
    bool isSkillCoolTime;
    #endregion

    #region Unity
    private void Update()
    {
        switch (nowState)
        {
            case State.None:
                nextState = State.Init;
                break;
            case State.Idle:
                SearchTarget();
                break;
            case State.Move:
                SearchTarget();
                if (curHp < maxHp / 2) MoveUnit();
                break;
            default:
                break;
        }
    }
    #endregion

    #region Override/new
    protected override void StateUpdate()
    {
        if (nowState == nextState) return;

        if (nowState == State.None)
        {
            switch (nextState)
            {
                case State.Init:
                    Init();
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
        }
        else if (nowState == State.Init)
        {
            switch (nextState)
            {
                case State.Idle:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
        }
        else if (nowState == State.Idle)
        {
            switch (nextState)
            {
                case State.Move:
                    break;
                case State.Attack:
                    break;
                case State.SkillCast:
                    break;
                case State.CC:
                    break;
                case State.Death:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("Idle", false);
        }
        else if (nowState == State.Move)
        {
            switch (nextState)
            {
                case State.Idle:
                    break;
                case State.Attack:
                    break;
                case State.SkillCast:
                    break;
                case State.CC:
                    break;
                case State.Death:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("Move", false);
        }
        else if (nowState == State.Attack)
        {
            switch (nextState)
            {
                case State.Idle:
                    break;
                case State.SkillCast:
                    break;
                case State.CC:
                    break;
                case State.Death:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("Attack", false);
        }
        else if (nowState == State.Death)
        {
            switch (nextState)
            {
                case State.Destory:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("Die", false);
        }
        else if (nowState == State.Destory)
        {
            switch (nextState)
            {
                default:
                    //nextState = nowState;
                    return;
            }
        }
        else if (nowState == State.SkillCast)
        {
            switch (nextState)
            {
                case State.Idle:
                    break;
                case State.CC:
                    break;
                case State.Death:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("Skill", false);
        }
        else if (nowState == State.CC)
        {
            switch (nextState)
            {
                case State.Idle:
                    break;
                case State.Death:
                    break;
                default:
                    //nextState = nowState;
                    return;
            }
            animator.SetBool("CC", false);
        }
        nowState = nextState;
        switch (nowState)
        {
            case State.Idle:
                animator.SetBool("Idle", true);
                break;
            case State.Move:
                animator.SetBool("Move", true);
                break;
            case State.Attack:
                AtkCoolTime();
                animator.SetBool("Attack", true);
                break;
            case State.Death:
                animator.SetBool("Die", true);
                break;
            case State.SkillCast:
                animator.SetBool("Skill", true);
                break;
            case State.CC:
                animator.SetBool("CC", true);
                break;
            default:
                break;
        }
    }
    protected new void Init()
    {
        base.Init();
    }
    #endregion

    #region private Method
    private void AnimEvent_Attack(int n)
    {
        Debug.Log(n);
        if (n == 1) //AttackTiming
        {
            TargetObj.GetDamage(curAtkPwr);
        }
        else if (n == 2) //End Attack
        {
            nextState = State.Idle;
        }
    }

    private void SearchTarget()
    {
        if (targetObj == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, curSrchRng);
            Unit closestUnit = null;
            float closestDistance = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                Unit unit = collider.GetComponent<Unit>();
                if (unit != null && unit.Team != this.Team)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestUnit = unit;
                    }
                }
            }

            if (closestUnit != null) targetObj = closestUnit;
            else
            {
                if (nowState == State.Idle && curSpeed != 0) nextState = State.Move;
                return;
            }
        }

        float dis = Vector3.Distance(transform.position, targetObj.transform.position);
        if (dis > curAtkRng && curSpeed != 0)
        {
            nextState = State.Move;
        }
        else if (dis <= curAtkRng)
        {
            if (!isAtkCoolTime) nextState = State.Attack;
        }

    }
    private void MoveUnit()
    {
        Vector3 newPos = new Vector3();
        if (targetObj == null)
        {
            transform.LookAt(InGameManager.Instance.tower.transform);
            newPos = Vector3.MoveTowards(transform.position, InGameManager.Instance.tower.transform.position, curSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(targetObj.transform);
            newPos = Vector3.MoveTowards(transform.position, targetObj.transform.position, curSpeed * Time.deltaTime);
        }

        rb.MovePosition(newPos);
    }
    private void AnimEvent_Die()
    {
        nextState = State.Death; return;
    }
    #endregion

}
