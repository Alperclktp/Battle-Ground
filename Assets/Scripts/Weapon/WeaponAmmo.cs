using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public WeaponSettings weaponSettingsSO;

    public float currentExtraAmmo;

    public int currentClipSize;

    private void Start()
    {
        currentExtraAmmo = weaponSettingsSO.ExtraAmmo;
        currentClipSize = weaponSettingsSO.ClipSize;

        weaponSettingsSO.CurrentAmmo = weaponSettingsSO.ClipSize;

    }

    public void Reload()
    {
        if (currentExtraAmmo >= weaponSettingsSO.ClipSize)
        {
            int ammoToReaload = (int)(weaponSettingsSO.ClipSize - weaponSettingsSO.CurrentAmmo);

            currentExtraAmmo -= ammoToReaload;

            weaponSettingsSO.CurrentAmmo += ammoToReaload;
        }
        else if (currentExtraAmmo > 0)
        {
            if (currentExtraAmmo + weaponSettingsSO.CurrentAmmo > weaponSettingsSO.ClipSize)
            {
                int leftOverAmmo = (int)(currentExtraAmmo + weaponSettingsSO.CurrentAmmo - weaponSettingsSO.ClipSize);

                currentExtraAmmo = leftOverAmmo;

                currentExtraAmmo = weaponSettingsSO.ClipSize;
            }
            else
            {
                weaponSettingsSO.CurrentAmmo += currentExtraAmmo;

                currentExtraAmmo = 0;
            }
        }
    }
}
    