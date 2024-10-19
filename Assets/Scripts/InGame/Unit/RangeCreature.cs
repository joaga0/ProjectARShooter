using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeCreature : Creature
{
    #region Serialize Field
    [SerializeField] Projectile projectile;
    [SerializeField] float projSpeed;
    #endregion

    #region Override Method
    protected override void AnimEvent_Attack(int n)
    {
        if (n == 1) //AttackTiming
        {
            Projectile tmp = Instantiate(projectile, this.transform.position, Quaternion.identity);
            tmp.init(targetObj, team, curAtkPwr, projSpeed);
        }
        else if (n == 2) //End Attack
        {
            nextState = State.Idle;
        }
    }
    #endregion

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
                MoveUnit();
                break;
            default:
                break;
        }
    }
}
