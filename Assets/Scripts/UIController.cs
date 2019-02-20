using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController pc;
    public Button startButton;
    public Text expText;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    public void StartGame()
    {
        this.gameObject.SetActive(false);
        pc.inGame = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        pc.inGame = false;
        expText.text = "You did it! Quit the game below.";
    }
}
