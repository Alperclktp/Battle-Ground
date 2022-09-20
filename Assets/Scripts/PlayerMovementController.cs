using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody rb;

    private Animator anim;

    [SerializeField] private Transform playerModel;

    [SerializeField] private float walkSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private bool canWalk;

    private Vector3 moveDirection;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        float inputY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(inputX, 0, inputY);

        Walk(inputX, inputY);

        //Rotation();
    }

    private void Walk(float inputX, float inputY)
    {
        if (moveDirection != Vector3.zero)
        {
            canWalk = true;

            //rb.velocity = new Vector3(inputX * walkSpeed, 0, inputY * walkSpeed);

            Vector3 movement = new Vector3(inputX, 0, inputY) * walkSpeed * Time.deltaTime;
            transform.Translate(movement, Space.Self);

            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);

            canWalk = false;
        }

        //anim.SetFloat("Speed", Vector3.ClampMagnitude(moveDirection, 1).magnitude, walkSpeed, Time.deltaTime * 10);

        //Blendtree'ye baðla ardýndan setfloat ile walk, run arasý geçiþ yap.
        //Mouse ile hareket ettirdiðimiz zaman karakter dönüyor ama mouse ile hareket ettirmeden hareket ettiðimizde karekter o yöne doðru
        //dönerek gitmeli

        //serbest bakýç aþýsýnda arkamýza baktýktan sonra býraktýðýmýz tekrar o yöne devam etmemiz gerekirken karakter arkaya doðru
        //dönüyor
    }

    private void Rotation()
    {
        Vector3 rotationOffset = Camera.main.transform.TransformDirection(moveDirection);

        rotationOffset.y = 0f;

        playerModel.forward = Vector3.Slerp(playerModel.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }
}
