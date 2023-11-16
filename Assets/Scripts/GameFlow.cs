using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    [Header("NextLevel")]
    public string NextLevel = "Level2";
    public int levelToUnlock = 2;
    [Header("References")]
    //public TMP_Text LivesText;
    public GameObject GameOverUI;
    public TMP_Text RoundsText;
    public GameObject PauseUI;
    public SceneFader fader;

    private int lives;
    public int Lives
    {
        private set
        {
            lives = value;

            //string livesText = " Life";
            //if (lives > 1)
            //{
            //    livesText = " Lives";
            //}

            //LivesText.text = lives + livesText;
        }
        get
        {
            return lives;
        }
    }

    public bool IsGameOver
    {
        get
        {
            return (Lives <= 0);
        }
    }

    private WaveManager waveManager;

    private void Start()
    {
        Lives = 1;
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseToggle();
        }
    }

    private void PauseToggle()
    {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoseLife()
    {
        if (Lives < 0)
            return;

        Lives--;

        if (Lives <= 0)
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        RoundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < waveManager.WaveIndex)
        {
            round++;
            RoundsText.text = round.ToString();
            yield return new WaitForSeconds(0.05f);
        }


        RoundsText.text = (waveManager.WaveIndex - 1).ToString();
        GameOverUI.SetActive(true);
    }

    public void Action_Retry()
    {
        if (!IsGameOver)
            PauseToggle();

        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Action_Menu()
    {
        if (!IsGameOver)
            PauseToggle();

        fader.FadeTo("MainMenu");
    }

    public void Action_Continue()
    {
        PauseToggle();
    }


    public void Action_NextLevel()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        fader.FadeTo(NextLevel);
    }

}
