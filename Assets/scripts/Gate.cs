using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool buttonPressed;
    
    [SerializeField]private GameObject gate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        { 
            gate.SetActive(false);
        }

        else if (!buttonPressed)
        {
            gate.SetActive(true);
        }
    }
}
