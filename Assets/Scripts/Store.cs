using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private ControladorJogo CJ;
    public Text currencyText;
    public Button buyMagnetUpgradeButton;
    public Button buyInvulnerabilityButton; // Botão para comprar invulnerabilidade
    public Button buyLightningAbilityButton; // Botão para comprar habilidade de raio
    public Text warningText; // Texto para exibir o aviso

    private bool invulnerabilityPurchased = false; // Verifica se a invulnerabilidade foi comprada
    private int lightningAbilityLevel = 0; // Nível da habilidade de raio

    void Start()
    {
        CJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorJogo>();
        UpdateCurrencyText();
        buyMagnetUpgradeButton.onClick.AddListener(BuyMagnetUpgrade);
        buyInvulnerabilityButton.onClick.AddListener(BuyInvulnerability); // Adiciona o listener para o botão de invulnerabilidade
        buyLightningAbilityButton.onClick.AddListener(BuyLightningAbility); // Adiciona o listener para o botão de habilidade de raio
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

    public void BuyInvulnerability()
    {
        int invulnerabilityCost = 100;

        if (CJ.moedas >= invulnerabilityCost && !invulnerabilityPurchased)
        {
            CJ.moedas -= invulnerabilityCost;
            UpdateCurrencyText();
            warningText.gameObject.SetActive(false); // Esconde o aviso se a compra for bem-sucedida
            invulnerabilityPurchased = true;

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.StartCoroutine(player.ActivateInvulnerability());
            }
        }
        else if (invulnerabilityPurchased)
        {
            warningText.text = "Invulnerabilidade já comprada.";
            warningText.gameObject.SetActive(true); // Mostra o aviso
            Invoke("HideWarningText", 2f); // Esconde o aviso após 2 segundos
        }
        else
        {
            warningText.text = "Moeda insuficiente para comprar a invulnerabilidade.";
            warningText.gameObject.SetActive(true); // Mostra o aviso
            Invoke("HideWarningText", 2f); // Esconde o aviso após 2 segundos
        }
    }

    public void BuyLightningAbility()
    {
        int lightningAbilityCost = 75 + (lightningAbilityLevel * 25); // Custo aumenta com o nível

        if (CJ.moedas >= lightningAbilityCost)
        {
            CJ.moedas -= lightningAbilityCost;
            UpdateCurrencyText();
            warningText.gameObject.SetActive(false); // Esconde o aviso se a compra for bem-sucedida
            lightningAbilityLevel++;

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.UpgradeLightningAbility(lightningAbilityLevel);
            }
        }
        else
        {
            warningText.text = "Moeda insuficiente para comprar a habilidade de raio.";
            warningText.gameObject.SetActive(true); // Mostra o aviso
            Invoke("HideWarningText", 2f); // Esconde o aviso após 2 segundos
        }
    }

    void HideWarningText()
    {
        warningText.gameObject.SetActive(false);
    }
}