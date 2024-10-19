using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Creature : Unit
{
    #region Override
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
        switch(nowState)
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
    #endregion

    #region Abstract Method
    protected abstract void AnimEvent_Attack(int n);
    #endregion

    #region Protected Method
    protected void SearchTarget()
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
            if(!isAtkCoolTime) nextState = State.Attack;
        }
        
    }
    protected void MoveUnit()
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
    protected void AnimEvent_Die()
    {
        nextState = State.Death; return;
    }
    #endregion
}
