
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(
            0,
            rotationSpeed * Time.deltaTime,
            0
        );

        transform.position = startPosition +
            Vector3.right *
            Mathf.Sin(Time.time * moveSpeed) *
            moveDistance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player =
                other.GetComponent<PlayerController>();

            if (player != null)
                player.Respawn();
        }
    }
}
