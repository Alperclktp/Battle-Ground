using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]

    [SerializeField] private float fireRate;
    [SerializeField] private bool semiAuto;
    private float fireRateTime;

    [Header("Bullet Properties")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrePos;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletPerShot;

    private AimStateManager aim;

    private void Start()
    {
        aim = GetComponentInParent<AimStateManager>();

        fireRateTime = fireRate;
    }

    private void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
    }

    private bool ShouldFire()
    {
        fireRateTime += Time.deltaTime;

        if(fireRateTime < fireRate) { return false; }

        if(semiAuto && Input.GetMouseButton(0)) { return true; }

        if(!semiAuto && Input.GetMouseButtonDown(0)) { return true; }
        else
        {
            return false;
        }
        
    }

    private void Fire()
    {
        fireRateTime = 0;

        barrePos.LookAt(aim.aimPos);

        for(int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrePos.position, barrePos.rotation);

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

            rb.AddForce(barrePos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
