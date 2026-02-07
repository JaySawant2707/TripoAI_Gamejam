using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(ThirdPersonController))]
public class PlayerSwimController : MonoBehaviour
{
    [Header("Swim Settings")]
    [SerializeField] private float swimSpeed = 2.5f;
    [SerializeField] private float swimTurnSpeed = 6f;


    private ThirdPersonController controller;
    private Animator animator;

    private float defaultMoveSpeed;
    private float defaultGravity;
    private float defaultJumpHeight;

    public bool IsSwimming { get; private set; }

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

        // Cache original values
        defaultMoveSpeed = controller.MoveSpeed;
        defaultGravity = controller.Gravity;
        defaultJumpHeight = controller.JumpHeight;
    }

    private void Update()
    {
        if (!IsSwimming) return;

        RotateWhileSwimming();
    }

    private void RotateWhileSwimming()
    {
        // Get camera forward direction (ignore vertical tilt)
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        if (camForward.sqrMagnitude < 0.01f) return;

        Quaternion targetRotation = Quaternion.LookRotation(camForward);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            swimTurnSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        EnterSwim();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        ExitSwim();
    }

    private void EnterSwim()
    {
        if (IsSwimming) return;

        IsSwimming = true;

        controller.MoveSpeed = swimSpeed;

        // Disable gravity & jumping
        controller.Gravity = defaultGravity;
        controller.JumpHeight = defaultJumpHeight;

        if (animator != null)
            animator.SetBool("IsSwimming", true);
    }

    private void ExitSwim()
    {
        if (!IsSwimming) return;

        IsSwimming = false;

        controller.MoveSpeed = defaultMoveSpeed;

        // Restore defaults
        controller.Gravity = defaultGravity;
        controller.JumpHeight = defaultJumpHeight;

        if (animator != null)
            animator.SetBool("IsSwimming", false);
    }
}
