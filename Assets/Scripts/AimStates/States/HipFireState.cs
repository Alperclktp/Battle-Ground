using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("isAiming", false);

        aim.currentFov = aim.hipFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetMouseButton(1))
        {
            aim.SwitchState(aim.Aim);

            //aim.rifleOne.transform.localEulerAngles = new Vector3(-90, 0, 0);
        }
    }
}
