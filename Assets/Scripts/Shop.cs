using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Attributes")]
    public int StartMoney;
    [Header("References")]
    public TMP_Text MoneyText;
 
    public event Action OnMoneyUpdated;
    public int Money { get { return money; } }
    private int money;

    public TurretBlueprint TurretToBuild { private set; get; }

    public bool CanBuild 
    { 
        get 
        {
            bool can = false;

            if (TurretToBuild && TurretToBuild.Cost <= money)
                can = true;

            return can;
        } 
    }

    private void Start()
    {
        UpdateMoney(StartMoney);
    }

    public void NodeSelected(Node node)
    {
        TurretToBuild = null;
    }

    public void ChooseTurret(TurretBlueprint turretBlueprint)
    {
        TurretToBuild = turretBlueprint;   
    }

    public void UpdateMoney(int value)
    {
        int startValue = money;
        money += value;

        OnMoneyUpdated();

        StartCoroutine(UpdateMoneyCoroutine(startValue, money));
    }

    private IEnumerator UpdateMoneyCoroutine(int startValue, int endValue)
    {
        int counter = startValue;
        float rate = Mathf.Abs(startValue - endValue);
        rate = Mathf.Min(0.01f, 1 / rate);

        if (startValue > endValue)
        {
            while (counter > endValue)
            {
                counter--;
                MoneyText.text = counter.ToString();
                yield return new WaitForSeconds(rate);
            }
        }
        else
        {
            while (counter < endValue)
            {
                counter++;
                MoneyText.text = counter.ToString();
                yield return new WaitForSeconds(rate);
            }
        }

        MoneyText.text = endValue.ToString();
    }
}
