using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    [ReadOnly] public float damageValue;

    private void Start()
    {
        //Destroy(this.gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);

        DealDamage(collision);
    }

    private void DealDamage(Collision collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageValue);
        }
    }
}
