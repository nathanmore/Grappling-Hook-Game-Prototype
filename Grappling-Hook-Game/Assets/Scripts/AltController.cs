using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 40f;
    [SerializeField] private float yClamp = 60f;

    float rotY = 0f;
    float rotX = 0f;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraLook();
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (context.performed)
        {
            Debug.Log("Hook");
        }
    }
    public void OnMouseX(InputAction.CallbackContext context)
    {
        rotX += context.ReadValue<float>() * sensitivity * Time.deltaTime;
        //rotX = Mathf.Clamp(rotX, minX, maxX);
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        rotY += context.ReadValue<float>() * sensitivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -yClamp, yClamp);
    }

    public void CameraLook()
    {
        transform.localEulerAngles = new Vector3(0, rotX, 0);
        mainCamera.transform.localEulerAngles = new Vector3(-rotY, 0, 0);
    }
}
