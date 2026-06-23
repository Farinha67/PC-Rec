using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 3f;
    public Transform holdPoint;
    public Inventory inventory;

    private GameObject heldObject;

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
        Ray ray = Camera.main.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;

                if (inventory != null)
                    inventory.AddItem();
                else
                    Debug.LogError("Inventory não conectado no PickupSystem!");

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.isKinematic = true;

                heldObject.transform.position = holdPoint.position;
                heldObject.transform.parent = holdPoint;
            }
        }
    }

    void Drop()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        heldObject.transform.parent = null;

        if (rb != null)
            rb.isKinematic = false;

        if (inventory != null)
            inventory.RemoveItem();

        heldObject = null;
    }

    public bool EstaSegurandoCaixa()
    {
        return heldObject != null;
    }

    public void DestruirCaixa()
    {
        if (inventory != null)
            inventory.RemoveItem();

        Destroy(heldObject);
        heldObject = null;
    }
}