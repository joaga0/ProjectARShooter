using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeCreature : Creature
{
    #region Override Method
    protected override void AnimEvent_Attack(int n)
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
