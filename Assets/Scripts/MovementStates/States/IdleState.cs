using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement.direction.magnitude  > 0.1)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.Run);
            }
            else
            {
                movement.SwitchState(movement.Walk);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            movement.SwitchState(movement.Crouch);
        }
    }
}
