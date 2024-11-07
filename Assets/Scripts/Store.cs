using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private ControladorJogo CJ;
    public Text currencyText;
    public Button buyMagnetUpgradeButton;
    public Text warningText; // Texto para exibir o aviso
    


    void Start()
    {
        CJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
        UpdateCurrencyText();
        buyMagnetUpgradeButton.onClick.AddListener(BuyMagnetUpgrade);
        warningText.gameObject.SetActive(false); // Inicialmente escondido
    }

    void UpdateCurrencyText()
    {
        currencyText.text = "Moeda: " + CJ.moedas.ToString();
    }

    public void BuyMagnetUpgrade()
    {
        int magnetUpgradeCost = 50;

        if (CJ.moedas >= magnetUpgradeCost)
        {
            CJ.moedas -= magnetUpgradeCost;
            UpdateCurrencyText();
            warningText.gameObject.SetActive(false); // Esconde o aviso se a compra for bem-sucedida

            Ima ima = FindObjectOfType<Ima>();
            if (ima != null)
            {
                ima.AumentarRaioAlcance(2f); // Aumenta o raio de alcance do ímã
            }
        }
        else
        {
            warningText.text = "Moeda insuficiente para comprar a melhoria.";
            warningText.gameObject.SetActive(true); // Mostra o aviso
            Invoke("HideWarningText", 2f); // Esconde o aviso após 2 segundos
        }
    }

    void HideWarningText()
    {
        warningText.gameObject.SetActive(false);
    }
}
