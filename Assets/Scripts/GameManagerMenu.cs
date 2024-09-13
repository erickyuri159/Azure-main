using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMenu : MonoBehaviour
{
   [SerializeField] private GameObject PainelMenu;
    [SerializeField] private GameObject PainelObcoes;

    public void AbrirOpcoes()
    {
        PainelMenu.SetActive(false);
        PainelObcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        PainelMenu.SetActive(true);
        PainelObcoes.SetActive(false);
    }
    public void SairJogo()
    {
        Application.Quit();
    }
}
