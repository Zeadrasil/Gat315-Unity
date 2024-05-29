using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool onGround;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float pushPower = 1.0f;
    [SerializeField] private float rotation = 3.0f;
    [SerializeField] private Transform view;
    [SerializeField] Animator animator;
    [SerializeField] Rig rig;
    private float gravity = -9.81f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        onGround = controller.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        move = Quaternion.Euler(0, view.rotation.eulerAngles.y, 0) * move;
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotation);
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Vector2 localMovement = new Vector2(move.x, move.z);
        Debug.Log(animator.GetBool("Equipped"));
        animator.SetFloat("Speed", localMovement.magnitude * speed);
        animator.SetFloat("YVelocity", velocity.y);
        animator.SetBool("OnGround", onGround);
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Equipped", !animator.GetBool("Equipped"));
            rig.weight = animator.GetBool("Equipped") ? 1 : 0;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.AddForce(pushDir * pushPower);
    }
}
