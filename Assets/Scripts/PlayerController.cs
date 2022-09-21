using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Animator anim;

    [SerializeField] private Transform playerModel;

    [SerializeField] private float walkSpeed;

    [SerializeField] private float runSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private bool canWalk;

    [SerializeField] private bool canRun;

    [SerializeField] private bool canCrouch;

    [SerializeField] private bool canCrouchWalk;

    [SerializeField] private bool canCrawl;

    [Header("FOV Settings")]
    [SerializeField] float currentFOV;

    [SerializeField] float maxFov;

    [SerializeField] float minFov;

    private Vector3 moveDirection;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        Crouch();

        AimHandleZoom();
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        float inputY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(inputX, 0, inputY);

        Walk(inputX, inputY);

        Run(inputX, inputY);

        CrouchWalk(inputX, inputY);

        //Rotation();
    }

    private void Walk(float inputX, float inputY)
    {
        if (moveDirection != Vector3.zero)
        {
            canWalk = true;

            //rb.velocity = new Vector3(inputX * walkSpeed, 0, inputY * walkSpeed);

            Vector3 moveWalk = new Vector3(inputX, 0, inputY) * walkSpeed * Time.deltaTime;
            transform.Translate(moveWalk, Space.Self);

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
    
    private void Run(float inputX, float inputY)
    {
        if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !canCrouch)
        {
            canRun = true;

            Vector3 moveRun = new Vector3(inputX, 0, inputY) * runSpeed * Time.deltaTime;

            transform.Translate(moveRun, Space.Self);

            anim.SetBool("isRunning", true);
        }
        else
        {
            canRun = false;

            anim.SetBool("isRunning", false);
        }
    }

    private void Crouch()
    {
        if (moveDirection == Vector3.zero && Input.GetKeyDown(KeyCode.C))
        {
            canCrouch = !canCrouch;
        }

        if (canCrouch)
        {
            walkSpeed = 0.6f;

            anim.SetBool("isCrouching", true);       
        }
        else
        {
            walkSpeed = 3;

            anim.SetBool("isCrouching", false);
        }
    }

    private void CrouchWalk(float inputX, float inputY)
    {
        if (moveDirection != Vector3.zero && canCrouch)
        {
            canCrouchWalk = true;

            Vector3 moveCrocuhWalk = new Vector3(inputX, 0, inputY) * walkSpeed * Time.deltaTime;

            transform.Translate(moveCrocuhWalk, Space.Self);

            anim.SetBool("isCrouchWalking", true);
        }
        else
        {
            canCrouchWalk = false;

            anim.SetBool("isCrouchWalking", false);
        }
    }

    private void Crawl()
    {
        //Sürünme.
    }

    private void Rotation()
    {
        Vector3 rotationOffset = Camera.main.transform.TransformDirection(moveDirection);

        rotationOffset.y = 0f;

        playerModel.forward = Vector3.Slerp(playerModel.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    public void AimHandleZoom()
    {
        Camera.main.fieldOfView = currentFOV;

        if (Input.GetMouseButtonDown(1))
        {
            DOTween.To(() => currentFOV, x => currentFOV = x, minFov, 0.2f);
        }

        if (Input.GetMouseButtonUp(1))
        {
            DOTween.To(() => currentFOV, x => currentFOV = x, maxFov, 0.2f);
        }
    }
}
