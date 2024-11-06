using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public int playerCurrency = 100; // Moeda inicial do jogador
    public Text currencyText;
    public Button buyMagnetUpgradeButton;
    public Text warningText; // Texto para exibir o aviso

    void Start()
    {
        UpdateCurrencyText();
        buyMagnetUpgradeButton.onClick.AddListener(BuyMagnetUpgrade);
        warningText.gameObject.SetActive(false); // Inicialmente escondido
    }

    void UpdateCurrencyText()
    {
        currencyText.text = "Moeda: " + playerCurrency;
    }

    public void BuyMagnetUpgrade()
    {
        int magnetUpgradeCost = 50;

        if (playerCurrency >= magnetUpgradeCost)
        {
            playerCurrency -= magnetUpgradeCost;
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
