using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public AudioSource exitSound;
    public AudioSource continueSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartClicked()
    {
        continueSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitClicked()
    {
        MusicData.currentTime = 0;
        Time.timeScale = 1f;
        exitSound.Play();
        SceneManager.LoadScene("LevelSelectScene");
    }
}
