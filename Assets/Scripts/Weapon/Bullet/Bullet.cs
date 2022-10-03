using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(10); //Vereceði damage deðerini WeaponManager'in içindeki currentDamage ile vermesi gerekiyor.
        }
    }
}
