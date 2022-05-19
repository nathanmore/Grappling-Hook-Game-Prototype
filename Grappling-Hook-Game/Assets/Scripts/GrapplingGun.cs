using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private Transform gunTip;

    private Vector3 grapplePoint;
    private LineRenderer lineRenderer;
    private SpringJoint joint;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Get LineRenderer component on gun
    }

    public void DrawRope(Vector3 grapplePos)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePos);
    }

    public void RemoveRope()
    {
        lineRenderer.enabled = false;
    }


    ///// <summary>
    ///// Called from PlayerController when grapple input detected.
    ///// Creates a raycast and sets a joint at the raycastHit point.
    ///// </summary>
    //public void StartGrapple()
    //{
    //    Debug.Log("Start Grapple");
    //    RaycastHit hit;
    //    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance))
    //    {
    //        grapplePoint = hit.point;
    //        joint = playerTransform.gameObject.AddComponent<SpringJoint>();
    //        joint.autoConfigureConnectedAnchor = false;
    //        joint.connectedAnchor = grapplePoint;

    //        float distanceFromPoint = Vector3.Distance(playerTransform.position, grapplePoint);

    //        joint.maxDistance = distanceFromPoint * 0.8f;
    //        joint.minDistance = distanceFromPoint * 0.25f;

    //        joint.spring = 20f;
    //        joint.damper = 7f;
    //        joint.massScale = 4.5f;

    //        playerRigidbody.MovePosition(grapplePoint.normalized * grappleSpeed * Time.deltaTime);
    //    }
    //}

    ///// <summary>
    ///// Draws the rope that is visible to player to show grappling.
    ///// Called in Update Function
    ///// </summary>
    //private void DrawRope()
    //{
    //    lineRenderer.SetPosition(0, gunTip.position);
    //    lineRenderer.SetPosition(1, grapplePoint);
    //}

    ///// <summary>
    ///// Is called when the grapple input button is released.
    ///// Removes all grappling effects.
    ///// </summary>
    //public void StopGrapple()
    //{
    //    Debug.Log("Stop Grapple");
    //}
}
