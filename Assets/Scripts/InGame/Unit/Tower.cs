using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    #region Serialize Field
    [SerializeField] GameObject towerBullet;
    #endregion

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
            //animator.SetBool("Idle", false);
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
            //animator.SetBool("Attack", false);
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
            //animator.SetBool("Die", false);
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
            //animator.SetBool("Skill", false);
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
            //animator.SetBool("CC", false);
        }
        nowState = nextState;
        switch (nowState)
        {
            case State.Idle:
               // animator.SetBool("Idle", true);
                break;
            case State.Attack:
                Attack();
               // animator.SetBool("Attack", true);
                break;
            case State.Death:
                //animator.SetBool("Die", true);
                break;
            case State.SkillCast:
                //animator.SetBool("Skill", true);
                break;
            case State.CC:
                //animator.SetBool("CC", true);
                break;
            default:
                break;
        }
    }
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
                //SearchTarget(); Attack?
                break;
            default:
                break;
        }
    }
    #endregion

    #region private
    private void SearchTarget()
    {
        if (targetObj == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, curAtkRng);
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
                return;
            }
        }

        float dis = Vector3.Distance(transform.position, targetObj.transform.position);
        if (dis > curAtkRng)
        {
            nextState = State.Move;
        }
        else if (dis <= curAtkRng)
        {
            if (!isAtkCoolTime) nextState = State.Attack;
        }

    }
    private void Attack()
    {
        if (isAtkCoolTime) return;

        //Projectile p = Instantiate(towerBullet, this.transform.position, Quaternion.identity).GetComponent<Projectile>();
        //p.init(targetObj, curAtkPwr);
        AtkCoolTime();
    }
    #endregion
}
