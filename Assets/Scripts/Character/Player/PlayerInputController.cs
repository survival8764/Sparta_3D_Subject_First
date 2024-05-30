using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    public Camera camera;

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 LookInput = value.Get<Vector2>();
        CallLookEvent(LookInput);
    }

    public void OnJump()
    {
        CallJumpEvent();
    }


}
