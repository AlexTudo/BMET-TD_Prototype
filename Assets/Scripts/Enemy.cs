using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject DeathEffect;
    public GameObject CoinReward;
    [Header("References")]
    public Image HealthBar;
    [Header("Stats")]
    public float startHealth = 100;
    public int value = 50;
    public float startSpeed = 15;

    public float MoveSpeed { set; get; }
    public bool IsDead { private set; get; }

    private WaveManager waveManager;
    private Animator animator;

    private void Start()
    {
        startSpeed += Random.Range(-2f, 2f);

        MoveSpeed = startSpeed;
        waveManager = FindObjectOfType<WaveManager>();
        animator = GetComponent<Animator>();

        currentHealth = startHealth;
    }

    private float currentHealth;

    public void TakeDamage(float amount)
    {
        //step 1



        //step 2



        //...


    }

    private void Die()
    {
        IsDead = true;

        MoveSpeed = 0;

        //shop.UpdateMoney(value);

        GameObject newCoin = Instantiate(CoinReward, transform.position, Quaternion.identity);
        newCoin.GetComponent<CoinReward>().SetValue(value);

        animator.SetBool("Dead", true);

        StartCoroutine(DelayedDestroy());

    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5);
        waveManager.DestroyEnemy(this);
    }

    private void Update()
    {
        animator.SetFloat("Speed", MoveSpeed);
    }

    //public void TakeDamage(float amount)
    //{
    //    if (IsDead)
    //        return;

    //    currentHealth -= amount;

    //    HealthBar.fillAmount = currentHealth / startHealth;

    //    if (currentHealth <= 0)
    //    {
    //        Destroy(gameObject);
    //        Die();
    //    }
    //}
}
