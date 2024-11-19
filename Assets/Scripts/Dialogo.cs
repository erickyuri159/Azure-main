using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogo : MonoBehaviour
{
    public string[] dialogueNpc;
    public Sprite[] dialogueImages; // Imagens correspondentes ao diálogo
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue = true;
    public float textSpeed = 0.04f; // Velocidade do texto
    public Button startGameButton; // Referência ao botão de iniciar o jogo
    public Button skipButton; // Referência ao botão de pular diálogo

    void Start()
    {
        startGameButton.gameObject.SetActive(false); // Esconde o botão no início
        skipButton.onClick.AddListener(SkipDialogue);

        if (PlayerPrefs.GetInt("DialogueSeen", 0) == 0)
        {
            StartDialogue();
            NextDialogue();
        }
        else
        {
            startGameButton.gameObject.SetActive(true); // Mostra o botão de iniciar jogo se o diálogo já foi visto
        }
        NextDialogue();
    }

    private void Update()
    {
        if (dialogueText.text == dialogueNpc[dialogueIndex])
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(delayMensagem());
        }
        else
        {
            startDialogue = false;
            dialogueIndex = 0;
            startGameButton.gameObject.SetActive(true);
            skipButton.gameObject.SetActive(false); // Esconde o botão de pular quando o diálogo termina
            PlayerPrefs.SetInt("DialogueSeen", 1); // Marca o diálogo como visto
        }
    }

    IEnumerator delayMensagem()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(showDialogue());
    }

    void StartDialogue()
    {
        nameNpc.text = "Atayla";
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        skipButton.gameObject.SetActive(true); // Mostra o botão de pular no início do diálogo
        StartCoroutine(showDialogue());
    }

    IEnumerator showDialogue()
    {
        dialogueText.text = "";
        if (dialogueIndex < dialogueImages.Length)
        {
            imageNpc.sprite = dialogueImages[dialogueIndex]; // Atualiza a imagem conforme a frase
        }
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed); // Use textSpeed para controlar a velocidade
        }
    }

    void SkipDialogue()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueNpc[dialogueIndex]; // Exibe a fala completa imediatamente
    }
}