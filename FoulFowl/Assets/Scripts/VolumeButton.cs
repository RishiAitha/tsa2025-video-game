using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Image volumeButton;
    public bool isMuted;
    public Sprite mutedSprite;
    public Sprite unmutedSprite;
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = MusicData.isBGMuted;
        audioSources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (isMuted)
            {
                audioSources[i].volume = 0f;
                volumeButton.sprite = mutedSprite;
            }
            else
            {
                audioSources[i].volume = 1f;
                volumeButton.sprite = unmutedSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeToggled()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (isMuted)
            {
                audioSources[i].volume = 1f;
                volumeButton.sprite = unmutedSprite;
            }
            else
            {
                audioSources[i].volume = 0f;
                volumeButton.sprite = mutedSprite;
            }
        }

        isMuted = !isMuted;
        MusicData.isBGMuted = isMuted;
    }
}
