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

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
            volumeButton.sprite = mutedSprite;
        }

        isMuted = !isMuted;
    }
}
