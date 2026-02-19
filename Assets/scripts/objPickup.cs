using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedUp;
    public Rigidbody rb;
    public float throwAmount;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crosshair1.SetActive(true);
            crosshair2.SetActive(false);
            interactable = false;
        }
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {

        if (interactable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                objTransform.parent = cameraTrans;
                rb.useGravity = false;
                pickedUp = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                objTransform.parent = null;
                rb.useGravity = true;
                pickedUp = false;
            }
            if (pickedUp)
            {
                if (Input.GetMouseButtonDown(1))
                { 
                    objTransform.parent = null;
                    rb.useGravity = true;
                    rb.linearVelocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                    pickedUp = false;
                }
            }
        }
    }

}
