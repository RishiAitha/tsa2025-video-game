using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public AudioSource exitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitClicked()
    {
        MusicData.currentTime = 0;
        Time.timeScale = 1f;
        exitSound.Play();
        SceneManager.LoadScene("LevelSelectScene");
    }
}
