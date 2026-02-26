using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class objPickup : MonoBehaviour
{

    [SerializeField] private Transform camtrans, grabPointTransform, grabPoint;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask buttonlayer;

    [SerializeField] private float throwForce;

    public GameObject crosshair1;
    public GameObject crosshair2;

    private Grabable currentlyGrabbed;

    [Header("Gate Button Stuff")]
    [SerializeField] private GameObject gate, button;
    [SerializeField] private Material mat1, mat2;
    private bool isOpen;
    


    private void Update()
    {
        ShowCrosshair();
    }

    public void ShowCrosshair()
    {
        float pickupDistance = 3f;
        if (Physics.Raycast(camtrans.position, camtrans.forward, out RaycastHit raycasthit, pickupDistance, pickUpLayer))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
        }
        else
        {
            crosshair1.SetActive(true);
            crosshair2.SetActive(false);
        }
    }

    public void PickUp(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        if (currentlyGrabbed == null)
        {
            float pickupDistance = 3f;
            if (Physics.Raycast(camtrans.position, camtrans.forward, out RaycastHit raycasthit, pickupDistance, pickUpLayer))
            {
                Debug.Log("Hit: " + raycasthit.collider.gameObject.name);
                if (raycasthit.transform.TryGetComponent(out currentlyGrabbed))
                {
                    currentlyGrabbed.Grab(grabPointTransform);
                    Debug.Log("Grabbed: " + raycasthit.collider.gameObject.name);
                }
            }
        }
        else
        { 
            Grabable lastGrab = currentlyGrabbed;
            currentlyGrabbed.Drop();

            Vector3 localx = grabPoint.GetComponent<GrabPoint>().mouseDelta.x * camtrans.right;
            Vector3 localy = grabPoint.GetComponent<GrabPoint>().mouseDelta.y * Vector3.up;
            Vector3 velocity = (localx + localy) * throwForce;

            lastGrab.GetComponent<Rigidbody>().linearVelocity = velocity;
            currentlyGrabbed = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 localx = grabPoint.GetComponent<GrabPoint>().mouseDelta.x * camtrans.right;
        Vector3 localy = grabPoint.GetComponent<GrabPoint>().mouseDelta.y * Vector3.up;
        Vector3 velocity = (localx + localy) * throwForce;

        if (currentlyGrabbed)
        {
            Gizmos.DrawLine(currentlyGrabbed.transform.position, currentlyGrabbed.transform.position + velocity);
        }
    }

    public void Button(InputAction.CallbackContext ctx)
    {
        float pickupDistance = 3f;
        if (!ctx.performed)
            return;
            if (Physics.Raycast(camtrans.position, camtrans.forward, out RaycastHit raycasthit, pickupDistance, buttonlayer) && !isOpen)
            {
                gate.SetActive(false);
                button.GetComponent<Renderer>().material = mat2;
                isOpen = true;
            }
            else
            {
                gate.SetActive(true);
                button.GetComponent<Renderer>().material = mat1;
                isOpen = false;
            }
    }
}
