using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public Image[] levelButtonImages = new Image[3];
    public Sprite[] levelSprites0 = new Sprite[5];
    public Sprite[] levelSprites1 = new Sprite[5];
    public Sprite[] levelSprites2 = new Sprite[5];

    public AudioSource BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        BGMusic.time = MusicData.currentTime;

        for (int i = 0; i < GameData.levelWinners.Length; i++)
        {
            Sprite[] currentSprites;
            if (i == 2)
            {
                currentSprites = levelSprites2;
            }
            else if (i == 1)
            {
                currentSprites = levelSprites1;
            }
            else
            {
                currentSprites = levelSprites0;
            }

            if (!GameData.levelsPlayed[i])
            {
                levelButtonImages[i].sprite = currentSprites[0];
            }
            else
            {
                levelButtonImages[i].sprite = currentSprites[GameData.levelWinners[i] + 1];
            }

            if (i > 0)
            {
                if (GameData.levelsPlayed[i - 1])
                {
                    // TODO: once other levels are made, add this line
                    // levelButtonImages[i].gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MusicData.currentTime = BGMusic.time;
    }
    public void BackPressed()
    {
        SceneManager.LoadScene("PlayerSelectScene");
    }

    public void LevelButtonPressed(int levelNum)
    {
        MusicData.currentTime = 0f;
        SceneManager.LoadScene("LevelScene" + levelNum);
    }
}
