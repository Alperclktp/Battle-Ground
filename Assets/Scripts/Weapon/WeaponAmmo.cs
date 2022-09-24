using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;

    public float extraAmmo;

    [HideInInspector] public float currentAmmo;

    private void Start()
    {
        currentAmmo = clipSize;
    }

    public void Reload()
    {
        if(extraAmmo >= clipSize)
        {
            int ammoToReaload = (int)(clipSize - currentAmmo);

            extraAmmo -= ammoToReaload;

            currentAmmo += ammoToReaload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int leftOverAmmo = (int)(extraAmmo + currentAmmo - clipSize);

                extraAmmo = leftOverAmmo;

                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;

                extraAmmo = 0;
            }
        }

    }
}
    