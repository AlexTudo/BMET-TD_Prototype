using UnityEngine;

[CreateAssetMenu]
public class TurretBlueprint : ScriptableObject
{
    public GameObject Prefab; 
    public string DisplayName;
    public string Level;
    public Sprite Icon;
    public int Cost;
    public int SellCost;
    public TurretBlueprint Upgrade;
}
