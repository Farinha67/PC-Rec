using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}