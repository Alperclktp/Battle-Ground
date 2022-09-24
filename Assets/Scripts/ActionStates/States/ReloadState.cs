using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.rHandAim.weight = 0f;

        actions.leftHandIK.weight = 0;

        actions.anim.SetTrigger("isReload");
    }

    public override void UpdateState(ActionStateManager actions)
    {
    
    }
}
