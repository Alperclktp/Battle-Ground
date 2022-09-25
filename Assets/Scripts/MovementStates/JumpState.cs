using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.previousState == movementStateManager.Idle)
        {
            movementStateManager.anim.SetTrigger("isIdleJump");
        }
        else if (movementStateManager.previousState == movementStateManager.Walk || movementStateManager.previousState == movementStateManager.Run)
        {
            movementStateManager.anim.SetTrigger("isRunJump");
        }
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.canJump && movementStateManager.IsGrounded())
        {
            movementStateManager.canJump = false;

            if (movementStateManager.horizontal == 0 && movementStateManager.vertical == 0)
            {
                movementStateManager.SwitchState(movementStateManager.Idle);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                movementStateManager.SwitchState(movementStateManager.Run);
            }
            else
            {
                movementStateManager.SwitchState(movementStateManager.Walk);
            }
        }
    }
}
