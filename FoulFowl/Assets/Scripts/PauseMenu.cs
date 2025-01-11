using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeClicked();
        }
    }

    public void ResumeClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ExitClicked()
    {
        MusicData.currentTime = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelectScene");
    }
}
