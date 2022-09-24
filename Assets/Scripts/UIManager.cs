using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public WeaponAmmo weaponAmmo;

    public TMP_Text currentAmmoText;
    public TMP_Text extraAmmoText;

    private void Update()
    {
        currentAmmoText.text = weaponAmmo.currentAmmo.ToString();
        extraAmmoText.text = " /" + weaponAmmo.extraAmmo.ToString();
    }
}
