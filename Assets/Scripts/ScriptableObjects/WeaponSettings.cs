using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Settings/New Weapon")]
public class WeaponSettings : ScriptableObject
{
    [SerializeField] private int id = default;
    public int ID { get { return id; } }

    [SerializeField] private string displayName;
    public string DisplayName { get { return name; } }

    [SerializeField] private Sprite icon = null;
    public Sprite Icon { get { return icon; } }

    [Space(5)]

    [Header("Properties")]

    [SerializeField] private float fireRate;
    public float FireRate { get { return fireRate; } }

    [SerializeField] private float bulletVelocity;
    public float BulletVelocity { get { return bulletVelocity; } }

    [SerializeField] private int bulletPerShot;
    public int BulletPerShot { get { return bulletPerShot; } }

    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    [HideInInspector] private float currentAmmo;
    public float CurrentAmmo { get; set; }
    //CurrentAmmo not scriptable.

    [Header("Bullet Capacity")]

    [SerializeField] private int clipSize;
    public int ClipSize { get { return clipSize; } }

    public float ExtraAmmo;

    [Header("Fire Mode")]

    public WeaponFireModeState weaponFireModeState;

    [Header("Sounds")]

    [SerializeField] private AudioClip shootSound;
    public AudioClip ShootSound { get { return shootSound; } }

    [SerializeField] private AudioClip emptyShootSound;
    public AudioClip EmptyShootSound { get { return emptyShootSound; } }

    [SerializeField] private AudioClip magLoadSound;
    public AudioClip MagLoadSound { get { return magLoadSound; } }

    [SerializeField] private AudioClip magUnLoadSound;
    public AudioClip MagUnLoadSound { get { return magUnLoadSound; } }

    [SerializeField] private AudioClip reloadSlideSound;
    public AudioClip ReloadSlideSound { get { return reloadSlideSound; } }
   
    //NOTE: Empty Sound Effect Add...

}

public enum WeaponFireModeState
{
    Auto,
    Single,

}