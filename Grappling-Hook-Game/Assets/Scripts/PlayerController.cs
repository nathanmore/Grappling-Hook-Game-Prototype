using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float yClamp = 60f;

    private enum State { Normal, Escaped }

    private float rotY = 0f;
    private float rotX = 0f;
    private Camera mainCamera;
    private Rigidbody rigidbodyComponent;
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        rigidbodyComponent = GetComponent<Rigidbody>();

        state = State.Normal;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                CameraLook();
                break;
            case State.Escaped:
                break;
        }
    }

    public void OnHook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Cursor.visible || Cursor.lockState != CursorLockMode.Confined)
            {
                state = State.Normal;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
            else
            {
                Debug.Log("Hook");
            }
        }
    }
    public void OnMouseX(InputAction.CallbackContext context)
    {
        rotX += context.ReadValue<float>() * sensitivity * Time.deltaTime;
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        rotY += context.ReadValue<float>() * sensitivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -yClamp, yClamp);
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            state = State.Escaped;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CameraLook()
    {
        transform.localEulerAngles = new Vector3(0, rotX, 0);
        mainCamera.transform.localEulerAngles = new Vector3(-rotY, 0, 0);
    }
}
