using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    private WeaponManager weaponManager;

    public DefaultState Default = new DefaultState();
    public ReloadState Reload = new ReloadState();

    public GameObject currentWeapon;

    [HideInInspector] public WeaponAmmo ammo;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;

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
        weaponManager.audioSource.PlayOneShot(weaponManager.SMG_MagLoadSound);
    }

    public void MagUnLoad()
    {
        weaponManager.audioSource.PlayOneShot(weaponManager.SMG_MagUnLoadSound);
    }

    public void ReloadSlide()
    {
        weaponManager.audioSource.PlayOneShot(weaponManager.SMG_ReloadSlideSoudd);
    }
}
