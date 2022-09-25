using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("isWalking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ExitState(movement, movement.Crouch);
        }
        else if(movement.direction.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }

        if(movement.vertical < 0)
        {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }
    }

    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("isWalking", false);
        movement.SwitchState(state);
    }
}
