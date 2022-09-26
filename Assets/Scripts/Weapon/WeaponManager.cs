using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    [SerializeField] private WeaponSettings weaponSettingsSO;

    private AimStateManager aim;
    private WeaponAmmo ammo;
    private ActionStateManager actions;
    private WeaponRecoil weaponRecoil;
    private WeaponBloom weaponBloom;

    public UIManager uIManager;

    private bool semiAuto;
    private float fireRateTime;

    [Header("Referances")]
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bullet;

    [Header("VFX")]
    public ParticleSystem muzzleFlashParticle;
    public ParticleSystem bulletShellParticle;

    //private Light muzzleFlashLight;

    //private float lightIntensity;

    //[SerializeField] private float lightReturnSpeed = 2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();
        weaponRecoil = GetComponent<WeaponRecoil>();
        weaponBloom = GetComponent<WeaponBloom>();

        //muzzleFlashLight = GetComponentInChildren<Light>();

        fireRateTime = weaponSettingsSO.FireRate;

        //muzzleFlashLight.intensity = 0f;

        //lightIntensity = muzzleFlashLight.intensity;
    }

    private void Update()
    {
        //if (ShouldFire() && Input.GetMouseButton(1)) WeaponManager'daki should fire a sað týk a bastýðýnda ateþ etmeyi ekle.
        //HipFireState deki rig weight ini tekrar düzelt.
        if (ShouldFire())
        {
            Fire();
        }

        SwitchWeaponMode(uIManager.currentWeaponModeText.text);

        //muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    private bool ShouldFire()
    {
        fireRateTime += Time.deltaTime;

        if (fireRateTime < weaponSettingsSO.FireRate) { return false; }

        if (ammo.weaponSettingsSO.CurrentAmmo == 0) { return false; }

        if (actions.currentState == actions.Reload) { return false; }

        if (weaponSettingsSO.weaponFireModeState == WeaponFireModeState.Auto && Input.GetMouseButton(0)) { return true; }

        if (weaponSettingsSO.weaponFireModeState == WeaponFireModeState.Single && Input.GetMouseButtonDown(0)) { return true; }
        else
        {
            return false;
        }
    }

    private void Fire()
    {
        fireRateTime = 0;

        firePos.LookAt(aim.aimPos);

        firePos.localEulerAngles = weaponBloom.BloomAngle(firePos);

        audioSource.PlayOneShot(weaponSettingsSO.ShootSound);

        weaponRecoil.TriggerRecoil();

        TriggerMuzzleFlash();
        TriggerBulletShell();

        ammo.weaponSettingsSO.CurrentAmmo--;

        for (int i = 0; i < weaponSettingsSO.BulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, firePos.position, firePos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(firePos.forward * weaponSettingsSO.BulletVelocity, ForceMode.Impulse);
        }
    }

    private void TriggerMuzzleFlash()
    {
        muzzleFlashParticle.Play();

        //muzzleFlashLight.intensity = lightIntensity;
    }

    private void TriggerBulletShell()
    {
        bulletShellParticle.Emit(1);
    }

    private void SwitchWeaponMode(string modeText)
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            semiAuto = !semiAuto;

            StartCoroutine(IEWeaponModeTextVisibiltyTime());
        }

        if (semiAuto)
        {
            weaponSettingsSO.weaponFireModeState = WeaponFireModeState.Auto;

            uIManager.currentWeaponModeText.text = modeText = "Mode: Auto";
        }
        else
        {
            weaponSettingsSO.weaponFireModeState = WeaponFireModeState.Single;

            uIManager.currentWeaponModeText.text = modeText = "Mode: Single";

        }
    }

    private IEnumerator IEWeaponModeTextVisibiltyTime()
    {
        uIManager.currentWeaponModeText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        uIManager.currentWeaponModeText.gameObject.SetActive(false);

    }
}
