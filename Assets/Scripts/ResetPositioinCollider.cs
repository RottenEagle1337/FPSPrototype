using UnityEngine;

public class ResetPositioinCollider : MonoBehaviour
{
    [SerializeField] private Transform playerStartTransform;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = playerStartTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = startPosition;
        }
    }
}
