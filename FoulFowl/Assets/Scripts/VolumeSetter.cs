using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    public float desiredVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AudioSource>().volume != 0)
        {
            GetComponent<AudioSource>().volume = desiredVolume;
        }
    }
}
