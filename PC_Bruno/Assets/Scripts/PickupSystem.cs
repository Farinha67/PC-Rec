using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdPoint;
    public Inventory inventory;

    private GameObject heldObject;
    private Item heldItem;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
                Pickup();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (heldObject != null)
                Drop();
        }
    }

    void Pickup()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                // Pega o objeto que possui o script Item
                heldItem = hit.collider.GetComponentInParent<Item>();

                if (heldItem == null)
                {
                    Debug.LogError("O objeto não possui o script Item!");
                    return;
                }

                heldObject = heldItem.gameObject;

                if (inventory != null)
                    inventory.AddItem();

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.isKinematic = true;

                heldObject.transform.position = holdPoint.position;
                heldObject.transform.SetParent(holdPoint);
            }
        }
    }

    void Drop()
    {
        if (heldObject == null)
            return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        heldObject.transform.SetParent(null);

        if (rb != null)
            rb.isKinematic = false;

        if (inventory != null)
            inventory.RemoveItem();

        heldObject = null;
        heldItem = null;
    }

    public void PegarObjeto(GameObject prefab)
    {
        if (heldObject != null)
            return;

        heldObject = Instantiate(prefab, holdPoint.position, holdPoint.rotation);
        heldObject.transform.SetParent(holdPoint);

        heldItem = heldObject.GetComponent<Item>();

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = true;

        if (inventory != null)
            inventory.AddItem();
    }

    public bool EstaSegurandoCaixa()
    {
        return heldItem != null && heldItem.tipo == TipoItem.Caixa;
    }

    public bool EstaSegurandoTicket()
    {
        return heldItem != null && heldItem.tipo == TipoItem.Ticket;
    }

    public void DestruirCaixa()
    {
        if (heldObject == null)
            return;

        if (inventory != null)
            inventory.RemoveItem();

        Destroy(heldObject);

        heldObject = null;
        heldItem = null;
    }

    public void DestruirObjeto()
    {
        if (heldObject == null)
            return;

        if (inventory != null)
            inventory.RemoveItem();

        Destroy(heldObject);

        heldObject = null;
        heldItem = null;
    }

    public GameObject GetObjetoNaMao()
    {
        return heldObject;
    }

    public Item GetItemNaMao()
    {
        return heldItem;
    }
}