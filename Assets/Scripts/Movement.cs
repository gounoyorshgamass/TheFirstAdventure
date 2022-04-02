using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.3f;
    [SerializeField]
    private float jumpForce = 2.0f;
    
    public LayerMask GroundLayer = 6;
    private Rigidbody rigidbody;
    private BoxCollider collider;
    private bool isGrounded
    {
        get {
            var bottomCenterPoint = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);
            return Physics.CheckCapsule(collider.bounds.center, bottomCenterPoint, collider.bounds.size.x / 2 * 0.9f, GroundLayer);
        }
    }

    private Vector3 movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            return new Vector3(horizontal, 0.0f, vertical);
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
    }

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            rigidbody.AddForce(movementVector * speed, ForceMode.Impulse);
        }
    }
    private void Jump()
    {
        if (isGrounded && (Input.GetAxis("Jump") > 0))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Run();
        Jump();
    }
}
