using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    private Transform target; // O alvo que a seta deve apontar
    private Camera mainCamera;

    public float distanceFromPlayer = 2.0f;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - mainCamera.transform.position;
            direction.z = 0; // Ignorar a diferença no eixo Z

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            
            Vector3 playerPosition = mainCamera.transform.position;

            playerPosition.z = 0; // Manter a seta no plano 2D

            Vector3 offset = direction.normalized * distanceFromPlayer;

            transform.position = playerPosition + offset;
        }
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
