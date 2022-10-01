using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [HideInInspector] public WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    public void Reload()
    {
        if (weaponManager.currentExtraAmmo >= weaponManager.currentClipSize)
        {
            int ammoToReaload = (int)(weaponManager.currentClipSize - weaponManager.weaponSettingsSO.CurrentAmmo);

            weaponManager.currentExtraAmmo -= ammoToReaload;

            weaponManager.weaponSettingsSO.CurrentAmmo += ammoToReaload;
        }
        else if (weaponManager.currentExtraAmmo > 0)
        {
            if (weaponManager.currentExtraAmmo + weaponManager.weaponSettingsSO.CurrentAmmo > weaponManager.currentClipSize)
            {
                int leftOverAmmo = (int)(weaponManager.currentExtraAmmo + weaponManager.weaponSettingsSO.CurrentAmmo - weaponManager.currentClipSize);

                weaponManager.currentExtraAmmo = leftOverAmmo;

                weaponManager.currentExtraAmmo = weaponManager.currentClipSize;
            }
            else
            {
                weaponManager.weaponSettingsSO.CurrentAmmo += weaponManager.currentExtraAmmo;

                weaponManager.currentExtraAmmo = 0;
            }
        }
    }
}
    