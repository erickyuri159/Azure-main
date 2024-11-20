using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public GameObject groundIndicatorPrefab; // Prefab para o indicador no chão
    public float timeBeforeFall = 2f; // Tempo antes de cair

    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = GetTargetPosition();
        GameObject indicator = Instantiate(groundIndicatorPrefab, targetPosition, Quaternion.identity);
        Destroy(indicator, timeBeforeFall); // Destrói o indicador após o tempo especificado
        StartCoroutine(FallAfterDelay());
    }

    Vector2 GetTargetPosition()
    {
        // Implementar lógica para determinar a posição alvo da bola de fogo
        // Por exemplo, uma posição aleatória no campo de batalha
        return new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f));
    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(timeBeforeFall);
        // Lançar a bola de fogo em direção ao alvo
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Implementar dano ao jogador
            other.GetComponent<Player>().ReduceHealthPoint(10);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // Destroi a bola de fogo ao atingir o chão
            Destroy(gameObject);
        }
    }
}
