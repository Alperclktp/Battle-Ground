using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    //private float timer;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void Update()
    {
        /*
        timer += Time.deltaTime;


        if(timer >= destroyTime)
        {
            Destroy(this.gameObject);
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
