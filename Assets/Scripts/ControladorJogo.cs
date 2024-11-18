using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControladorJogo : MonoBehaviour
{
    public float moedas;
    public Text moedasText; // Adicione esta linha
    // Start is called before the first frame update
    void Start()
    {
       
        if (PlayerPrefs.HasKey("moeda"))
        {
            moedas = PlayerPrefs.GetFloat("moeda");
        }
        else
        {
            moedas = 0; // Inicializa a variável localmente
           PlayerPrefs.SetFloat("moeda", moedas);
        }
        AtualizarTextoMoedas(); // Atualiza o texto no início
    }

    // Update is called once per frame
    
    public void GanhaMoedas(float Novamoedas)
    {
        moedas = PlayerPrefs.GetFloat("moeda");
        moedas = moedas + Novamoedas;
        PlayerPrefs.SetFloat("moeda", moedas);
        AtualizarTextoMoedas(); // Atualiza o texto sempre que ganhar moedas
    }
    void AtualizarTextoMoedas()
    {
        if (moedasText != null)
        {
            moedasText.text = "Moedas: " + moedas.ToString();
        }
    }
}
