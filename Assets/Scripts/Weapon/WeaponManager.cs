using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public WeaponSettings weaponSettingsSO;

    private AimStateManager aim;
    private WeaponAmmo ammo;
    private ActionStateManager actions;
    private WeaponRecoil weaponRecoil;
    private WeaponBloom weaponBloom;

    public UIManager uIManager;

    private bool semiAuto;

    private float fireRateTime;

    [Header("Weapon Settings")]
    public int currentID;
    public string currentName;
    public Sprite currentIcon;
    public float currentFireRate;
    public float currentBulletVelocity;
    public int currentBulletPerShot;
    public float currentDamage;
    public int currentClipSize;
    public float currentExtraAmmo;

    [Header("Referances")]
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bullet;

    [Header("VFX")]
    public ParticleSystem muzzleFlashParticle;
    public ParticleSystem bulletShellParticle;

    [Header("VFX Settings")]
    [SerializeField] private float lightReturnSpeed = 0.5f;

    private Light muzzleFlashLight;

    private float lightIntensity;

    private void Start()
    {
        GetWeaponData();

        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();
        weaponRecoil = GetComponent<WeaponRecoil>();
        weaponBloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = GetComponentInChildren<Light>();

        fireRateTime = currentFireRate;

        lightIntensity = muzzleFlashLight.intensity;

        muzzleFlashLight.intensity = 0f;
    }

    private void Update()
    {
        //if (ShouldFire() && Input.GetMouseButton(1)) WeaponManager'daki should fire a sað týk a bastýðýnda ateþ etmeyi ekle.
        //HipFireState deki rig weight ini tekrar düzelt.
        if (ShouldFire())
        {
            Fire();
        }

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);

        SwitchWeaponMode(uIManager.currentWeaponModeText.text);
    }

    public void GetWeaponData()
    {
        currentID = weaponSettingsSO.ID;
        currentName = weaponSettingsSO.DisplayName;
        currentIcon = weaponSettingsSO.Icon;
        currentFireRate = weaponSettingsSO.FireRate;
        currentBulletVelocity = weaponSettingsSO.BulletVelocity;
        currentBulletPerShot = weaponSettingsSO.BulletPerShot;
        currentClipSize = weaponSettingsSO.ClipSize;
        currentExtraAmmo = weaponSettingsSO.ExtraAmmo;

        currentDamage = weaponSettingsSO.Damage;

        weaponSettingsSO.CurrentAmmo = weaponSettingsSO.ClipSize;
    }

    public void DealDamage(Collision collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(currentDamage);
        }
    }

    private bool ShouldFire()
    {
        fireRateTime += Time.deltaTime;

        if (fireRateTime < currentFireRate) { return false; }

        if (ammo.weaponManager.weaponSettingsSO.CurrentAmmo == 0) { return false; }

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

        ammo.weaponManager.weaponSettingsSO.CurrentAmmo--;

        for (int i = 0; i < currentBulletPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, firePos.position, firePos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            currentBullet.GetComponent<Bullet>().damageValue = currentDamage;
            rb.AddForce(firePos.forward * currentBulletVelocity, ForceMode.Impulse);
        }
    }

    private void TriggerMuzzleFlash()
    {
        muzzleFlashParticle.Play();
        muzzleFlashLight.intensity = lightIntensity;
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
