using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    [SerializeField] float sensitivityX = 15f;
    [SerializeField] float sensitivityY = 0.2f;
    [SerializeField] private Transform debugHitPointTransform;
    [SerializeField] private float hookshotSpeed = 5f;
    [SerializeField] private float reachedHookshotPositionDistance;
    [SerializeField] private float gravityScale;

    private CharacterController characterController;
    private float mouseX, mouseY;
    private float xRotation = 0f;
    private State state;
    private Vector3 hookshotPosition;

    private enum State { Normal,  HookshotFlyingPlayer}

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default: 
            case State.Normal:
                CameraMovement();
                ApplyGravity();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                CameraMovement();
                break;
        }
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Hook");
            HandleHookshotStart();
        }
    }
    public void OnMouseX(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<float>() * sensitivityX;
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        mouseY = context.ReadValue<float>() * sensitivityY;
    }

    public void CameraMovement()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    private void ApplyGravity()
    {
        characterController.Move(Vector3.up * -gravityScale);
    }

    private void HandleHookshotStart()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit))
        {
            // Hit something
            debugHitPointTransform.position = raycastHit.point;
            state = State.HookshotFlyingPlayer;
            hookshotPosition = raycastHit.point;
        }
    }

    private void HandleHookshotMovement()
    {
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        characterController.Move(hookshotDir * hookshotSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            // Reached Hookshot Position
            state = State.Normal;
        }
    }


}
