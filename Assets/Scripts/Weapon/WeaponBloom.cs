using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] private float defaultBloomAngle;
    [SerializeField] private float walkBloomMultiplier;
    [SerializeField] private float crouchBloomMultiplier;
    [SerializeField] private float sprintBloomMultiplier;
    [SerializeField] private float adsBloomMultiplier;

    private MovementStateManager movement;
    private AimStateManager aiming;

    private float currentBloom;

    private void Start()
    {
        movement = GetComponentInParent<MovementStateManager>();
        aiming = GetComponentInParent<AimStateManager>();
    }

    public Vector3 BloomAngle(Transform firePos)
    {
        if (movement.currentState == movement.Idle) { currentBloom = defaultBloomAngle; }
        else if (movement.currentState == movement.Walk) { currentBloom = defaultBloomAngle * walkBloomMultiplier; }
        else if (movement.currentState == movement.Run) { currentBloom = defaultBloomAngle * sprintBloomMultiplier; }
        else if (movement.currentState == movement.Crouch)
        {
            if (movement.direction.magnitude == 0) { currentBloom = defaultBloomAngle * crouchBloomMultiplier; }
            else
            {
                currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
            }
        }

        if (aiming.currentState == aiming.Aim) { currentBloom *= adsBloomMultiplier; }

        float randomX = Random.Range(-currentBloom, currentBloom);
        float randomY = Random.Range(-currentBloom, currentBloom);
        float randomZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotation = new Vector3(randomX, randomY, randomZ);

        return firePos.localEulerAngles + randomRotation;

    }
}
