using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CoinReward : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private Rigidbody rbody;
    private Shop shop;

    private int moneyToEarn;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        rbody.AddForce(Vector3.up * 6, ForceMode.Impulse);

        shop = FindObjectOfType<Shop>();
    }

    public void SetValue(int value)
    {
        moneyToEarn = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        shop.UpdateMoney(moneyToEarn);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Mouse.current.leftButton.isPressed)
        {
            shop.UpdateMoney(moneyToEarn);
            Destroy(gameObject);
        }
    }
}
