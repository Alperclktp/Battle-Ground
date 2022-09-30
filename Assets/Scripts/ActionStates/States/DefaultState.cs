using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {

    }

    public override void UpdateState(ActionStateManager actions)
    {
        actions.rightHandAim.weight = Mathf.Lerp(actions.rightHandAim.weight, 1, 10 * Time.deltaTime);
        actions.leftHandIK.weight = Mathf.Lerp(actions.leftHandIK.weight, 1, 10 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }

        if(actions.ammo.weaponSettingsSO.CurrentAmmo == 0 && actions.ammo.currentExtraAmmo != 0)
        {
            actions.SwitchState(actions.Reload);
        }
    }

    private bool CanReload(ActionStateManager action)
    {
        if (action.ammo.weaponSettingsSO.CurrentAmmo == action.ammo.currentClipSize) { return false; }
        else if (action.ammo.currentExtraAmmo == 0) { return false; }
        else return true;
    }
}
