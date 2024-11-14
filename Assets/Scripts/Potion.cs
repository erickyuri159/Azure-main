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
                player.UpdateHealthBar(); // Chama o método para atualizar a barra de HP
                player.PlayHealingEffect(); // Chama o método para reproduzir o efeito de cura
                Destroy(gameObject); // Destrói a poção após ser usada
            }
        }
    }
}

