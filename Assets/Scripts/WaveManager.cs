using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Prefabs")]
    public List<WaveBlueprint> Waves;
    [Header("References")]
    public Transform SpawnPoint;
    public TMP_Text WaveCountdownText;
    public SceneFader fader;
    public GameObject CompleteLevel;
    public Button ReadyButton;
    [Header("Stats")]
    private float timeBetweenWaves = 5f;


    //public List<GameObject> Enemies { private set; get; }
    public List<Enemy> Enemies { private set; get; } = new List<Enemy>();
    public int WaveIndex { private set; get; }

    private float countdown = 5f;

    private bool levelFinished = false;

    private bool countdownStarted = false;

    private void Update()
    {
        if (levelFinished)
        {
            return;
        }

        if (Enemies.Count > 0)
        {
            return;
        }

        if (WaveIndex == Waves.Count)
        {
            levelFinished = true;
            CompleteLevel.SetActive(true);
            return;
        }

        if (countdown <= 0)
        {
            countdownStarted = false;
            countdown = timeBetweenWaves;
            WaveCountdownText.text = "";
            ReadyButton.interactable = false;
            StartCoroutine(SpawnWave());
            return;
        }

        if (countdownStarted)
        {
            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);

            WaveCountdownText.text = "Next wave: " + string.Format("{0:00.00}", countdown);
        }
    }

    private IEnumerator SpawnWave()
    {
        WaveBlueprint wave = Waves[WaveIndex];

        for (int i = 0; i < wave.Count; i++)
        {
            SpawnEnemy(wave.EnemyPrefab);
            yield return new WaitForSeconds(1 / wave.Rate);
        }

        WaveIndex++;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        GameObject newEnemy = Instantiate(prefab, SpawnPoint.position, SpawnPoint.rotation);
        Enemies.Add(newEnemy.GetComponent<Enemy>());
    }

    public void DestroyEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        Destroy(enemy.gameObject);

        if (Enemies.Count <= 0)
        {
            ReadyButton.interactable = true;
        }
    }

    public void Action_Forward()
    {
        if (Enemies.Count > 0)
        {
            return;
        }

        if (!countdownStarted)
        {
            countdownStarted = true;
        }
        else
        {
            countdown = 0;
        }
    }
}
