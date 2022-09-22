using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("isCrouching", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if(movement.direction.magnitude < 0.1f)
            {
                ExitState(movement, movement.Idle);
            }
            else
            {
                ExitState(movement, movement.Walk);
            }
        }

        if (movement.vertical < 0)
        {
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.crouchSpeed;
        }
    }


    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("isCrouching", false);
        movement.SwitchState(state);
    }
}
