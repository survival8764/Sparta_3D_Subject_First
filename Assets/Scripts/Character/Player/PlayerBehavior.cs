using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private CharacterController controller;

     [Header("Movement")]
    public float moveSpeed;
    public float jumptForce;
    public LayerMask groundLayerMask;
    private Vector2 movementDirection = Vector2.zero;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    public Action inventory;

    private Rigidbody rigidbody;

    private void Awake()
    {
        controller = GetComponent<PlayerInputController>();
        rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Start() 
    {
        controller.OnMoveEvent += DirectionSetting;
        controller.OnJumpEvent += Jump;
        controller.OnLookEvent += DirectionMouseSetting;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * movementDirection.y + transform.right * movementDirection.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.AddForce(dir - rigidbody.velocity * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    private void DirectionSetting(Vector2 curMovementInput)
    {
        movementDirection = curMovementInput;
    }

    private void DirectionMouseSetting(Vector2 curMouseDeltaInput)
    {
        mouseDelta = curMouseDeltaInput;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        float rayDistance = 1.0f; // Ray의 길이를 늘려서 테스트

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        foreach (Ray ray in rays)
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 5.0f);
            Debug.Log($"Ray origin: {ray.origin}, direction: {ray.direction}, rayDistance: {rayDistance}");

            if (Physics.Raycast(ray, rayDistance, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    
}
