using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader fader;

    public void Action_Play()
    {
        fader.FadeTo("LevelSelect");
    }

    public void Action_Quit()
    {
        Application.Quit();
    }
}
