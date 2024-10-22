using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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



    void Start()
    {
        startGameButton.gameObject.SetActive(false);// Esconde o botão no início
        StartDialogue();

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
            // dialoguePanel.SetActive( false);
            startDialogue = false;
            dialogueIndex = 0;
            startGameButton.gameObject.SetActive(true);

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
        StartCoroutine(showDialogue());
    }

    IEnumerator showDialogue()
    {
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

    }



}