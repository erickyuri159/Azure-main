using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
   
    [Header("------- AudioSouce----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("------- Audio Clip----------")]
    public AudioClip background;
    public AudioClip Death;
    public AudioClip efeitos;
    public AudioClip morteMonstros;
    public AudioClip raio;
    public AudioClip outros;
}


