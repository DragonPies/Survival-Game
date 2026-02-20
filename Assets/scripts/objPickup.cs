using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objPickup : MonoBehaviour
{

    [SerializeField] private Transform camtrans, grabPointTransform;
    [SerializeField] private LayerMask pickUpLayer;

    private Grabable currentlyGrabbed;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                currentlyGrabbed.Drop();
                currentlyGrabbed = null;
            }
        }
    }

}
