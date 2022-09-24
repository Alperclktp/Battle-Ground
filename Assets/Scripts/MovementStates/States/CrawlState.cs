using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrawlState : MonoBehaviour //MovementBaseState
{
    /*
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("isCrawling", true);

        movement.playerRig.weight = 0.5f;

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (movement.direction.magnitude < 0.1f)
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
        movement.anim.SetBool("isCrawling", false);

        movement.playerRig.weight = 1;

        movement.SwitchState(state);
    }
    */
}
