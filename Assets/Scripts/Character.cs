using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float crouchSpeed = 2.0f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 0.8f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;
    public float crouchColliderHeight = 1.35f;

    [Header("Animation Smoothing")]
    [Range(0, 1)] public float speedDampTime = 0.1f;
    [Range(0, 1)] public float velocityDampTime = 0.9f;
    [Range(0, 1)] public float rotationDampTime = 0.2f;
    [Range(0, 1)] public float airControl = 0.5f;

    [Header("Drone")]
    public bool isMouseAiming;

    //states
    public StateMachine movementSM;
    public IdleState idle;
    public CrouchState crouch;
    public LandState land;
    public SprintState sprint;
    public CombatState combat;
    public AttackState attack;
    //public SprintJumpState sprintjumping;
    //public JumpState jump;

    [HideInInspector] public float gravityValue = -9.81f;
    [HideInInspector] public float normalColliderHeight;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Transform cameraTransform;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Vector3 playerVelocity;


    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;

        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        crouch = new CrouchState(this, movementSM);
        land = new LandState(this, movementSM);
        sprint = new SprintState(this, movementSM);
        combat = new CombatState(this, movementSM);
        attack = new AttackState(this, movementSM);
        //jump = new JumpState(this, movementSM);
        //sprintjumping = new SprintJumpState(this, movementSM);

        movementSM.Initialize(idle);

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            isMouseAiming = true;
        }
        else
        {
            isMouseAiming = false;
        }
        movementSM.currentState.HandleInput();

        movementSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.currentState.PhysicsUpdate();
    }
}
