using UnityEngine;

public class Camera : MonoBehaviour
{
    private float mouseInputX;
    private float mouseInputY;


    objPickup o;

    [SerializeField] private float Sensetivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseInputX += Input.GetAxis("Mouse X") * Sensetivity;

        mouseInputY += Input.GetAxis("Mouse Y") * -1 * Sensetivity;
        mouseInputY = Mathf.Clamp(mouseInputY, -90, 90);

        transform.localEulerAngles = new Vector3(mouseInputY, mouseInputX, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag("Item"))
        {
        Debug.Log("See Object");
            o.crosshair1.SetActive(false);
            o.crosshair2.SetActive(true);
            o.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Item"))
        {
        Debug.Log("Does Not See Object");
            o.crosshair1.SetActive(true);
            o.crosshair2.SetActive(false);
            o.interactable = false;
        }
    }


}
