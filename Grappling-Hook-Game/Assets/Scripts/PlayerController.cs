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

    CharacterController characterController;
    float mouseX, mouseY;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        Debug.Log("Hook");
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


}
