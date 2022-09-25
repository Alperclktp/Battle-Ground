using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public WeaponAmmo weaponAmmo;

    public TMP_Text currentAmmoText;
    public TMP_Text extraAmmoText;

    public TMP_Text currentWeaponModeText;

    private void Update()
    {
        currentAmmoText.text = weaponAmmo.weaponSettingsSO.CurrentAmmo.ToString();
        extraAmmoText.text = "/" + weaponAmmo.currentExtraAmmo.ToString();
    }
}
