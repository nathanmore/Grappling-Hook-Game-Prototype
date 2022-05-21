using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float yClamp = 60f;
    [SerializeField] float maxGrappleDistance = 100f;
    [SerializeField] float grappleSpeed = 50f;
    [SerializeField] float reachedGrapplePositionDistance = 3f;
    [SerializeField] LayerMask whatIsGrappleable;

    private enum State { Normal, Escaped, Grappling }

    private float rotY = 0f;
    private float rotX = 0f;
    private Camera mainCamera;
    private Rigidbody rigidbodyComponent;
    private GrapplingGun grapplingGun;
    private Vector3 grapplePoint;
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        rigidbodyComponent = GetComponent<Rigidbody>();
        grapplingGun = GetComponentInChildren<GrapplingGun>();

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
            case State.Grappling:
                CameraLook();
                HandleGrappleMovement();
                break;
        }
    }

    private void LateUpdate()
    {
        switch (state)
        {
            default:
            case State.Normal:
                grapplingGun.RemoveRope();
                break;
            case State.Escaped:
                break;
            case State.Grappling:
                grapplingGun.DrawRope(grapplePoint);
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
                StartGrapple();
            }
        }
        if (context.canceled)
        {
            StopGrapple();
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

    private void StartGrapple()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, maxGrappleDistance, whatIsGrappleable))
        {
            state = State.Grappling;
            grapplePoint = hit.point;
        }
    }

    private void HandleGrappleMovement()
    {
        Vector3 grappleDir = (grapplePoint - transform.position).normalized;

        float speedAdjustment = Vector3.Distance(transform.position, grapplePoint);
        Mathf.Clamp(speedAdjustment, 10, 100);

        rigidbodyComponent.velocity = grappleDir * grappleSpeed * speedAdjustment * Time.deltaTime * 10f;

        if (Vector3.Distance(transform.position, grapplePoint) < reachedGrapplePositionDistance)
        {
            state = State.Normal;
        }
    }

    private void StopGrapple()
    {
        state = State.Normal;
    }
}
