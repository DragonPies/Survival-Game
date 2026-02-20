using UnityEngine;

public class UI : MonoBehaviour
{
    public bool winGame;

    public GameObject winScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (winGame)
        {
            Debug.Log("You win!");
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winScreen.SetActive(true);
    }
}
