using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("isAiming", false);
        //MovementStateManager.Instance.playerRig.weight = 0;
        aim.currentFov = aim.hipFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetMouseButton(1))
        {
            aim.SwitchState(aim.Aim);

            //MovementStateManager.Instance.playerRig.weight = 1;

            aim.currentWeapon.transform.localEulerAngles = new Vector3(aim.currentWeapon.transform.localEulerAngles.x, aim.currentWeapon.transform.localEulerAngles.y, 270);    
        }
    }
}
