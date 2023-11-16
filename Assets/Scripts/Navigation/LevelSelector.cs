using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public List<Button> LevelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < LevelButtons.Count; i++)
        {
            if (i + 1 > levelReached)
                LevelButtons[i].interactable = false;


        }
    }

    public void Action_Select (string levelName)
    {
        fader.FadeTo(levelName);
    }
}
