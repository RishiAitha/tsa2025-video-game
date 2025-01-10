using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackPressed()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }

    public void LevelButtonPressed(int levelNum)
    {
        SceneManager.LoadScene("LevelScene" + levelNum);
    }
}
