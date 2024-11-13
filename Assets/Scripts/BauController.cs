using System.Collections;
using UnityEngine;

public class BauController : MonoBehaviour
{
    public float tempoVisivel = 120f; // Tempo em segundos que o baú fica visível
    public GameObject moedaPrefab; // Prefab da moeda
    public GameObject pocaoPrefab; // Prefab da poção de vida
    public GameObject setaPrefab; // Prefab da seta
    private bool podeSerDestruido = false;
    private GameObject seta; // Referência à seta instanciada

    void Start()
    {
        // Instancia a seta e define o alvo como o baú
        seta = Instantiate(setaPrefab);
        seta.GetComponent<ArrowPointer>().SetTarget(transform);

        StartCoroutine(DesaparecerAposTempo());
    }

    IEnumerator DesaparecerAposTempo()
    {
        yield return new WaitForSeconds(tempoVisivel);
        Destroy(gameObject); // Destrói o baú após o tempo definido
        if (seta != null)
        {
            seta.GetComponent<ArrowPointer>().DestroyArrow(); // Destrói a seta
        }
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
            if (seta != null)
            {
                seta.GetComponent<ArrowPointer>().DestroyArrow(); // Destrói a seta
            }
        }
    }

    public void SetPodeSerDestruido(bool valor)
    {
        podeSerDestruido = valor;
    }
}
