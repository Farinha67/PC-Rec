using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float floatHeight = 0.2f;
    [SerializeField] private float floatSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        transform.position = startPos +
            Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
    }
}