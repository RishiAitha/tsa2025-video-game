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
    public AudioSource BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        isMuted = MusicData.isBGMuted;
        BGMusic = FindObjectOfType<AudioSource>();

        if (isMuted)
        {
            BGMusic.volume = 0f;
            volumeButton.sprite = mutedSprite;
        }
        else
        {
            BGMusic.volume = 0.1f;
            volumeButton.sprite = unmutedSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeToggled()
    {
        if (isMuted)
        {
            volumeButton.sprite = unmutedSprite;
            BGMusic.volume = 0.1f;
        }
        else
        {
            volumeButton.sprite = mutedSprite;
            BGMusic.volume = 0f;
        }

        isMuted = !isMuted;
        MusicData.isBGMuted = isMuted;
    }
}
