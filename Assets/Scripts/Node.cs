using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Prefabs")]
    public GameObject BuildEffect;
    public GameObject SellEffect;
    [Header("References")]
    public Renderer Rend;
    [Header("Attributes")]
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;
    private Color startColor;
    private Shop Shop;

    public TurretBlueprint LastTurretBuilt { private set; get; }

    private void Start()
    {
        Shop = FindObjectOfType<Shop>();
        startColor = Rend.material.color;
    }

    public Vector3 BuildPosition { get { return transform.position + positionOffset; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (turret != null)
        {
            SelectTurret();
            return;
        }

        if (!Shop.CanBuild)
            return;

        BuildTurret(Shop.TurretToBuild);
    }

    private void SelectTurret()
    {
        Shop.NodeSelected(this);
    }

    public void BuildTurret(TurretBlueprint turretToBuild)
    {
        LastTurretBuilt = turretToBuild;

        if (turret)
        {
            Destroy(turret);
        }

        turret = Instantiate(turretToBuild.Prefab, BuildPosition, Quaternion.identity);
        //GameObject effect = Instantiate(BuildEffect, BuildPosition, Quaternion.identity);   //TODO: improve this effect?
        //Destroy(effect, 5);

        Shop.UpdateMoney(-turretToBuild.Cost);
    }

    public void RemoveTurret()
    {
        GameObject effect = Instantiate(SellEffect, BuildPosition, Quaternion.identity);
        Destroy(effect, 5);

        Destroy(turret);

        LastTurretBuilt = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Shop.CanBuild)
            return;

        if (turret != null)
        {
            //TODO: highlight turret
            return;
        }

        Rend.material.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Rend.material.color = startColor;
    }
}
