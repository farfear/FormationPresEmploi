using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource m_music;
    public AudioSource m_dialogSFX;
    public Slider m_musicSlider;
    public Slider m__sfxSlider;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartSFX()
    {
        m_dialogSFX.Play();
    }

    public void StopSFX()
    {
        m_dialogSFX.Stop();
    }

    public void StartMusic()
    {
        m_music.Play();
    }

    public void StopMusic()
    {
        m_music.Stop();
    }

    public void MusicVolume()
    {
        m_music.volume = m_musicSlider.value;
    }

    public void SFXVolume()
    {
        m_dialogSFX.volume = m__sfxSlider.value;
    }
}
