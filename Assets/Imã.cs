using UnityEngine;

public class Imã : MonoBehaviour
{
    public float forçaAtração = 10f; // A força de atração das moedas
    public float raioAlcance = 5f; // O raio de alcance do ímã

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("crystal"))
        {
            Rigidbody2D rbcrystal = other.GetComponent<Rigidbody2D>();

            // Se a moeda tem um Rigidbody2D, move ela em direção ao ímã
            if (rbcrystal != null)
            {
                Vector2 direção = transform.position - other.transform.position;
                rbcrystal.velocity = direção.normalized * forçaAtração;
            }
        }
        if (other.CompareTag("moeda"))
        {
            Rigidbody2D rbMoeda = other.GetComponent<Rigidbody2D>();

            // Se a moeda tem um Rigidbody2D, move ela em direção ao ímã
            if (rbMoeda != null)
            {
                Vector2 direção = transform.position - other.transform.position;
                rbMoeda.velocity = direção.normalized * forçaAtração;
            }
        }
    }
}
