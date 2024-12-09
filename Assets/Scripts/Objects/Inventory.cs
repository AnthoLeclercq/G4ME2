using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Item> content = new List<Item>();

    private int contentCurrentIndex = 0;
    public Image itemImage;
    public Sprite emptyItemImage;
    public Text itemName;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;
    }

    private void Start()
    {
        UpdateInventoryImage();
    }

    public void ConsumeItem()
    {     
        if(content.Count == 0)
            return;

        Item currentItem = content[contentCurrentIndex];
        PlayerEffects.instance.AddSpeed(currentItem.speedGiven, currentItem.speedDuration);

        if(currentItem.name == "Heart")
        {
            if(PlayerLife.instance.currentLive < PlayerLife.instance.maxLives)
            {
                PlayerLife.instance.currentLive += currentItem.liveGiven;
                content.Remove(currentItem);
                GetNextItem();
                UpdateInventoryImage();
            }
        }
        else
        {
            content.Remove(currentItem);
            GetNextItem();
            UpdateInventoryImage();
        }
    }

    public void GetNextItem()
    {
        if(content.Count == 0)
            return;

        contentCurrentIndex++;
        if(contentCurrentIndex > content.Count - 1)
            contentCurrentIndex = 0;
        UpdateInventoryImage();
    }

    public void GetPreviousItem()
    {
        if(content.Count == 0)
            return;

        contentCurrentIndex--;
        if(contentCurrentIndex < 0)
            contentCurrentIndex = content.Count - 1;
        UpdateInventoryImage();
    }

    public void UpdateInventoryImage()
    {
        if(content.Count > 0)
        {
            itemImage.sprite = content[contentCurrentIndex].image;
            itemName.text = content[contentCurrentIndex].name;
        }
        else
        {
            itemImage.sprite = emptyItemImage;
            itemName.text = "";
        }
    }
}
