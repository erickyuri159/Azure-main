using UnityEngine;

public class Potion : MonoBehaviour
{
    public float healthRecoveryPercentage = 0.15f; // Percentual de vida a ser recuperado

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                int healthToRecover = Mathf.RoundToInt(player.GetMaxHealthPoint() * healthRecoveryPercentage);
                player.RecoverHealthPoint(healthToRecover);
                Destroy(gameObject); // Destrói a poção após ser usada
            }
        }
    }
}
