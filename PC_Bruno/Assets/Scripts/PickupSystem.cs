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
        // PEGAR
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
            {
                Pickup();
            }
        }

        // SOLTAR
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (heldObject != null)
            {
                Drop();
            }
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

                inventory.AddItem();

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                }

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
        {
            rb.isKinematic = false;
        }

        inventory.RemoveItem();

        heldObject = null;
    }

    // VERIFICA SE ESTÁ SEGURANDO
    public bool EstaSegurandoCaixa()
    {
        return heldObject != null;
    }

    // DESTROI CAIXA ENTREGUE
    public void DestruirCaixa()
    {
        inventory.RemoveItem();

        Destroy(heldObject);

        heldObject = null;
    }
}