using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banco : MonoBehaviour
{

    public float MeuCaixa;


    // Start is called before the first frame update
    void Start()
    {
        ///Esse comando apaga tudo
        ///////PlayerPrefs.DeleteAll();

        MeuCaixa = PlayerPrefs.GetFloat("moeda");


        //Existe uma Variavel que salva Nave?
        if (PlayerPrefs.HasKey("Personagem"))
        {
            //não faça nada
        }
        else
        {
            //Se Não Existe Crie uma
            PlayerPrefs.SetString("Personagem", "Basico");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Comprar(int tipo)
    {
        float custo = 0;
        string tipopersona = "";

        //Informações

        switch (tipo)
        {
            case 1:
                //Personagem Capuz
                custo = 1000;
                tipopersona = "Personagem1";
                break;

            case 2:
                //lagarto com calca
                custo = 5000;
                tipopersona = "Personagem2";
                break;

           

            default:
                custo = 0;
                tipopersona = "Basico";
                break;

        }
        if (MeuCaixa >= custo)
        {
            MeuCaixa = MeuCaixa - custo;
            PlayerPrefs.SetFloat("moeda", MeuCaixa);
            PlayerPrefs.SetString("Personagem1", tipopersona);
            BotaoComprado(tipo);
        }
        else
        {
            //nãocomprou
        }

    }

    public void Selecionar(int tipo)
    {

        string tipopersona = "";
        //Informações
        switch (tipo)
        {
            case 1:
                //lagarto albino
                tipopersona = "1";
                PlayerPrefs.SetString("Personagem1Escolhido", tipopersona);
                break;

            case 2:
                //lagarto com calca
                tipopersona = "2";
                PlayerPrefs.SetString("Personagem2Escolhido", tipopersona);
                break;
            

            default:

                tipopersona = "Basico";
                break;
        }
    }
    void BotaoComprado(int numeroBotao)
    {
        switch (numeroBotao)
        {

            case 1:
                PlayerPrefs.SetString("Botao1", "Comprado");
                break;
            case 2:
                PlayerPrefs.SetString("Botao2", "Comprado");
                break;
            case 3:
                PlayerPrefs.SetString("Botao3", "Comprado");
                break;
            case 4:
                PlayerPrefs.SetString("Botao4", "Comprado");
                break;

        }
    }
}