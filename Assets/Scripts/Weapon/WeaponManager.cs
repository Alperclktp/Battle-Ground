using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponModeState
{
    Auto,
    Single
}

public class WeaponManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public WeaponModeState weaponModeState;

    private AimStateManager aim;
    private WeaponAmmo ammo;
    private ActionStateManager actions;
    private WeaponRecoil weaponRecoil;
    private WeaponBloom weaponBloom;

    public UIManager uIManager;

    [Header("Fire Rate")]
    [SerializeField] private float fireRate;
    private bool semiAuto;
    private float fireRateTime;

    [Header("Bullet Properties")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletPerShot;

    [Header("Weapon Sounds")]
    [SerializeField] private AudioClip SMG_Shoot_Sound;
    [SerializeField] private AudioClip SMG_Empty_Shoot_Sound;
    public AudioClip SMG_MagLoadSound;
    public AudioClip SMG_MagUnLoadSound;
    public AudioClip SMG_ReloadSlideSoudd;

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

        fireRateTime = fireRate;

        //muzzleFlashLight.intensity = 0f;

        //lightIntensity = muzzleFlashLight.intensity;
    }

    private void Update()
    {
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

        if (fireRateTime < fireRate) { return false; }

        if (ammo.currentAmmo == 0) { return false; }

        if (actions.currentState == actions.Reload) { return false; }

        if (weaponModeState == WeaponModeState.Auto && Input.GetMouseButton(0)) { return true; }

        if (weaponModeState == WeaponModeState.Single && Input.GetMouseButtonDown(0)) { return true; }
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

        audioSource.PlayOneShot(SMG_Shoot_Sound);

        weaponRecoil.TriggerRecoil();

        TriggerMuzzleFlash();
        TriggerBulletShell();

        ammo.currentAmmo--;

        for (int i = 0; i < bulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, firePos.position, firePos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(firePos.forward * bulletVelocity, ForceMode.Impulse);
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
            weaponModeState = WeaponModeState.Auto;

            uIManager.currentWeaponModeText.text = modeText = "Mode: Auto";
        }
        else
        {
            weaponModeState = WeaponModeState.Single;

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