using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    // Referencias a los AudioSource
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Listas de música y efectos de sonido
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    // Función para reproducir música
    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Count)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("El índice de música está fuera de rango.");
        }
    }

    // Función para reproducir un efecto de sonido
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Count)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning("El índice del efecto de sonido está fuera de rango.");
        }
    }
}

