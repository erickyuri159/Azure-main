using UnityEngine;

public class Ima : MonoBehaviour
{
    public float forcaAtracao = 10f; // A força de atração das moedas
    public float raioAlcance = 5f; // O raio de alcance do ímã

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("crystal"))
        {
            Rigidbody2D rbCrystal = other.GetComponent<Rigidbody2D>();

            // Se o crystal tem um Rigidbody2D, move ele em direção ao ímã
            if (rbCrystal != null)
            {
                Vector2 direcao = transform.position - other.transform.position;
                rbCrystal.linearVelocity = direcao.normalized * forcaAtracao;
            }
        }
        else if (other.CompareTag("moeda"))
        {
            Rigidbody2D rbMoeda = other.GetComponent<Rigidbody2D>();

            // Se a moeda tem um Rigidbody2D, move ela em direção ao ímã
            if (rbMoeda != null)
            {
                Vector2 direcao = transform.position - other.transform.position;
                rbMoeda.linearVelocity = direcao.normalized * forcaAtracao;
            }
        }
    }

    public void AumentarRaioAlcance(float aumento)
    {
        raioAlcance += aumento;
        GetComponent<CircleCollider2D>().radius = raioAlcance;
        //Debug.Log("Raio de alcance do ímã aumentado para: " + raioAlcance);
    }
}
