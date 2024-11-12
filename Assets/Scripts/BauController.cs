using System.Collections;
using UnityEngine;

public class BauController : MonoBehaviour
{
    public float tempoVisivel = 120f; // Tempo em segundos que o baú fica visível
    public GameObject moedaPrefab; // Prefab da moeda
    public GameObject pocaoPrefab; // Prefab da poção de vida
    private bool podeSerDestruido = false;

    void Start()
    {
        StartCoroutine(DesaparecerAposTempo());
    }

    IEnumerator DesaparecerAposTempo()
    {
        yield return new WaitForSeconds(tempoVisivel);
        Destroy(gameObject); // Destrói o baú após o tempo definido
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (podeSerDestruido && other.CompareTag("Player"))
        {
            // Lógica para recompensas aleatórias
            if (Random.value > 0.5f)
            {
                Instantiate(moedaPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject pocao = Instantiate(pocaoPrefab, transform.position, Quaternion.identity);
                // Configura poção para recuperar 15% da vida do jogador
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    int healthToRecover = Mathf.RoundToInt(player.GetMaxHealthPoint() * 0.15f);
                    player.RecoverHealthPoint(healthToRecover);
                }
            }
            Destroy(gameObject); // Destrói o baú após ser destruído pelo jogador
        }
    }

    public void SetPodeSerDestruido(bool valor)
    {
        podeSerDestruido = valor;
    }
}
