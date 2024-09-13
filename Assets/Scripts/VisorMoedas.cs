using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class VisorMoedas : MonoBehaviour
{
    // Start is called before the first frame update
    private ControladorJogo CJ;
    private TMP_Text MeuText;


    void Start()
    {
        CJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
        MeuText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MeuText.text = "moedas: " + CJ.moedas.ToString();
    }
}
