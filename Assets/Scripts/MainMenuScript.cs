using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public void StartGame()
    {
        Application.LoadLevel(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Controls()
    {
        Application.LoadLevel(2);
    }
    public void Menu()
    {
        Application.LoadLevel(0);
    }
}
