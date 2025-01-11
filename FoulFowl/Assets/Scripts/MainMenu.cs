using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource BGMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        BGMusic.time = MusicData.currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        MusicData.currentTime = BGMusic.time;
    }

    public void GameStarted()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
