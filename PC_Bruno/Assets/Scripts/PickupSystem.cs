using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 3f;

    public Transform holdPoint;

    private GameObject heldObject;

    void Update()
    {
        // PEGAR OBJETO
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (heldObject == null)
            {
                Pickup();
            }
        }

        // SOLTAR OBJETO
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
        // Raycast no centro da tela
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            Debug.Log("Acertou: " + hit.collider.name);

            // Verifica se tem a tag Pickup
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                // Move pro HoldPoint
                heldObject.transform.position = holdPoint.position;

                // Faz virar filho do HoldPoint
                heldObject.transform.parent = holdPoint;
            }
        }
    }

    void Drop()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();

        // Remove da mão
        heldObject.transform.parent = null;

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        heldObject = null;
    }
}