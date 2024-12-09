using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string nameItem;
    public string description;
    public Sprite image;
    public int liveGiven;
    public int speedGiven;
    public float speedDuration;
}
