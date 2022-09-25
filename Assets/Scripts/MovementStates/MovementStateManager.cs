using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementStateManager : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;

    public MovementBaseState previousState;
    public MovementBaseState currentState;

    [HideInInspector] public Animator anim;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState();

    [Header("Player Movement Settings")]
    public float currentMoveSpeed;
    public float walkSpeed, walkBackSpeed;
    public float runSpeed, runBackSpeed;
    public float crouchSpeed, crouchBackSpeed;
    public float airSpeed = 1.5f;

    [HideInInspector] public float horizontal, vertical;

    [HideInInspector] public Vector3 direction;

    [Header("Check Ground")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    [HideInInspector] public bool canJump;

    private Vector3 spherePos;

    private Vector3 velocity;

    //public CrawlState Crawl = new CrawlState();

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
    }

    private void Start()
    {
        SwitchState(Idle);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        GetDirectionMove(horizontal, vertical);

        Gravity();

        Falling();

        SetAniamtions();

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
        
    }

    private void GetDirectionMove(float inputX, float inputY)
    {
        Vector3 airDirection = Vector3.zero;

        if (!IsGrounded())
        {
            airDirection = transform.forward * inputY + transform.right * inputX;
        }
        else
        {
            direction = transform.forward * inputY + transform.right * inputX;
        }
    
        controller.Move((direction.normalized * currentMoveSpeed + airDirection.normalized * airSpeed) * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void SetAniamtions()
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    public void JumpForce() => velocity.y += jumpForce;

    private void Falling() => anim.SetBool("isFalling", !IsGrounded());

    public void Jumped() => canJump = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
