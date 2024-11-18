using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupText : MonoBehaviour
{
    public Text itemText;
    public float displayDuration = 2f; // Duração em segundos que o texto será exibido

    void Start()
    {
        if (itemText != null)
        {
            itemText.enabled = false; // Inicialmente invisível
        }
    }

    public void ShowItemText(string itemName)
    {
        if (itemText != null)
        {
            itemText.text = "Você pegou: " + itemName;
            StartCoroutine(DisplayText());
        }
    }

    private IEnumerator DisplayText()
    {
        itemText.enabled = true;
        yield return new WaitForSeconds(displayDuration);
        itemText.enabled = false;
    }
}
