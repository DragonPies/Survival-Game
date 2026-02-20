using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Rigidbody rb;
    private Transform grabPointTransform;
    public UI ui;

    private void Awake()
        {
            rb = GetComponent<Rigidbody>();
    }
    public void Grab(Transform grabPointTransform)
    {
        this.grabPointTransform = grabPointTransform;
        rb.useGravity = false;
    }

    public void Drop()
    {
        this.grabPointTransform = null;
        rb.useGravity = true;
    }


    private void FixedUpdate()
    {
        if (grabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
       CompareTag("Winplatform");
        if (collision.gameObject.CompareTag("Winplatform"))
        {
            ui.winGame = true;
        }
    }
}
