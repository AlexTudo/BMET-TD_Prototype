using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [Header("Prefabs")]
    public TurretBlueprint blueprint;
    [Header("References")]
    public Image Icon;
    public TMP_Text PriceText;

    private Button Button;
    private Shop shop;

    void Start()
    {
        shop = FindObjectOfType<Shop>();
        shop.OnMoneyUpdated += OnMoneyUpdated;

        Button = GetComponent<Button>();

        Icon.sprite = blueprint.Icon;
        PriceText.text = blueprint.Cost.ToString();
    }

    private void OnMoneyUpdated()
    {
        if (shop.Money < blueprint.Cost)
        {
            Button.interactable = false;
            PriceText.color = Color.red;
        }
        else
        {
            Button.interactable = true;
            PriceText.color = Color.white;
        }
    }

    public void Action_ShopItem()
    {
        shop.ChooseTurret(blueprint);
    }
}
