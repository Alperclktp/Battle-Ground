using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Transform recoilFollowPos;

    [SerializeField] private float recoilBackAmount;

    [SerializeField] private float recoilBackSpeed, returnSpeed;

    private float currentRecoilPosition;

    private float finalRecoilPosition;

    private void Update()
    {
        currentRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, returnSpeed * Time.deltaTime);

        finalRecoilPosition = Mathf.Lerp(finalRecoilPosition, currentRecoilPosition, recoilBackSpeed * Time.deltaTime);

        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPosition);

    }

    public void TriggerRecoil() => currentRecoilPosition += recoilBackAmount;

} 

