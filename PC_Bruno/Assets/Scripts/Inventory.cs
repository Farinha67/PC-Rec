using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image slotImage;
    public Sprite caixaSprite;

    public void AddItem()
    {
        slotImage.sprite = caixaSprite;
        slotImage.color = Color.white;
    }

    public void RemoveItem()
    {
        slotImage.sprite = null;
        slotImage.color = new Color(1, 1, 1, 0);
    }
}