using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [SerializeField] private WeaponSettings weaponSettingsSO;

    [HideInInspector] public ActionBaseState currentState;
    [HideInInspector] public WeaponAmmo ammo;
    [HideInInspector] public Animator anim;

    public DefaultState Default = new DefaultState();
    public ReloadState Reload = new ReloadState();

    private WeaponManager weaponManager;

    public GameObject currentWeapon;

    [Header("Animationg Rigging IK")]
    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint leftHandIK;

    private void Start()
    {
        SwitchState(Default);

        anim = GetComponent<Animator>();
        weaponManager = GetComponentInChildren<WeaponManager>();
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void MagLoad()
    {
        weaponManager.audioSource.PlayOneShot(weaponSettingsSO.MagLoadSound);
    }

    public void MagUnLoad()
    {
        weaponManager.audioSource.PlayOneShot(weaponSettingsSO.MagUnLoadSound);
    }

    public void ReloadSlide()
    {
        weaponManager.audioSource.PlayOneShot(weaponSettingsSO.ReloadSlideSound);
    }
}
