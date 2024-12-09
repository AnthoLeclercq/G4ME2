using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    //private Text interactUI;
    private bool isInRange;
    public Item item;

    private void Update()
    {
        if(isInRange)
            TakeItem();
    }

    private void TakeItem()
    {
        Inventory.instance.content.Add(item);
        Inventory.instance.UpdateInventoryImage();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            isInRange = false;
    }
}
