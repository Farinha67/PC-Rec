using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image slotImage;
    public Sprite caixaSprite;

    void Start()
    {
        if (slotImage == null)
        {
            Debug.LogError("Slot Image não conectado!");
            return;
        }

        if (caixaSprite == null)
        {
            Debug.LogError("Caixa Sprite não conectado!");
            return;
        }

        RemoveItem();
    }

    public void AddItem()
    {
        if (slotImage == null || caixaSprite == null)
        {
            Debug.LogError("Falta conectar Slot Image ou Caixa Sprite!");
            return;
        }

        slotImage.sprite = caixaSprite;
        slotImage.color = Color.white;

        Debug.Log("Item adicionado no inventário");
    }

    public void RemoveItem()
    {
        if (slotImage == null) return;

        slotImage.sprite = null;
        slotImage.color = new Color(1, 1, 1, 0);
    }
}