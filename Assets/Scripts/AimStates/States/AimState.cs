using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("isAiming", true);

        aim.currentFov = aim.adsFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetMouseButtonUp(1))
        {
            aim.SwitchState(aim.HipFire);

            aim.rifleOne.transform.localEulerAngles = new Vector3(-90, 0, 40);
        }
    }
}
