using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementStateManager : MonoBehaviour
{
    [HideInInspector] public CharacterController controller;

    public MovementBaseState currentState;

    [HideInInspector] public Animator anim;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [Header("Player Movement Settings")]
    public float currentMoveSpeed;
    public float walkSpeed, walkBackSpeed;
    public float runSpeed, runBackSpeed;
    public float crouchSpeed, crouchBackSpeed;

    [HideInInspector] public float horizontal, vertical;

    [HideInInspector] public Vector3 direction;

    [Header("Check Ground")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;

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
        direction = transform.forward * inputY + transform.right * inputX;
        controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y <= 0)
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

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) { return true; }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
