using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    // Array para armazenar as músicas
    public AudioClip[] musicas;

    // Referência ao componente AudioSource
    private AudioSource audioSource;

    void Start()
    {
        // Obtém o componente AudioSource do objeto
        audioSource = GetComponent<AudioSource>();

        // Verifica se há músicas na lista
        if (musicas.Length > 0)
        {
            // Seleciona uma música aleatória para começar
            int indiceMusicaAleatoria = Random.Range(0, musicas.Length);
            AudioClip musicaAleatoria = musicas[indiceMusicaAleatoria];

            // Define a música selecionada para tocar
            audioSource.clip = musicaAleatoria;

            // Toca a música
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Nenhuma música encontrada na lista!");
        }
    }

    // Método para trocar de música
    public void TrocarMusica()
    {
        // Seleciona uma música aleatória da lista
        int indiceMusicaAleatoria = Random.Range(0, musicas.Length);
        AudioClip novaMusica = musicas[indiceMusicaAleatoria];

        // Define a nova música para tocar
        audioSource.clip = novaMusica;

        // Toca a nova música
        audioSource.Play();
    }

    // Método para pausar ou retomar a reprodução da música
    public void AlternarPausarContinuar()
    {
        // Verifica se a música está tocando
        if (audioSource.isPlaying)
        {
            // Pausa a reprodução da música
            audioSource.Pause();
        }
        else
        {
            // Continua a reprodução da música
            audioSource.Play();
        }
    }
}

