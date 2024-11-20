using UnityEngine;
using System.Collections;

public class AtaqBoss : MonoBehaviour
{
    Enemy character;
    Coroutine coroutine;
    Animator animator;
    public float attackRange = 2.0f; // Dist�ncia para iniciar o ataque
    public GameObject fireballPrefab; // Prefab da bola de fogo
    public Transform firePoint; // Ponto de origem da bola de fogo
    public GameObject groundMarkerPrefab; // Prefab do marcador no solo
    public float fireballSpeed = 10.0f; // Velocidade da bola de fogo
    public float markerLifetime = 2.0f; // Tempo de vida do marcador no solo

    void Awake()
    {
        character = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayerInRange())
        {
            if (coroutine == null && gameObject.activeSelf)
            {
                coroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    bool IsPlayerInRange()
    {
        GameObject player = Player.GetInstance().gameObject;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= attackRange;
    }

    IEnumerator Attack()
    {
        while (true)
        {
            GiveDamage();
            animator.SetTrigger("Attack"); // Chama a anima��o de ataque
            yield return new WaitForSeconds(1.0f); // Intervalo entre ataques
            LaunchFireball();
        }
    }

    void GiveDamage()
    {
        int damage = character.GetAttackPower() - (int)(character.GetAttackPower() * Player.GetInstance().GetDefencePower() / 100f);
        Player.GetInstance().ReduceHealthPoint(damage);
    }

    void LaunchFireball()
    {
        GameObject player = Player.GetInstance().gameObject;
        Vector3 targetPosition = player.transform.position;

        // Iniciar a Coroutine para lan�ar a bola de fogo com atraso
        StartCoroutine(LaunchFireballWithDelay(targetPosition));
    }

    IEnumerator LaunchFireballWithDelay(Vector3 targetPosition)
    {
        // Marcar o solo onde a bola de fogo cair�
        GameObject marker = Instantiate(groundMarkerPrefab, targetPosition, Quaternion.identity);
        Destroy(marker, markerLifetime); // Destruir o marcador ap�s o tempo especificado

        // Esperar 2 segundos antes de lan�ar a bola de fogo
        yield return new WaitForSeconds(2.0f);

        // Lan�ar a bola de fogo
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 direction = (targetPosition - firePoint.position).normalized;
            rb.linearVelocity = direction * fireballSpeed;
        }
    }
}
