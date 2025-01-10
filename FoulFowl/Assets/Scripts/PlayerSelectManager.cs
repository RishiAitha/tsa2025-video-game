using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    public Button player3Toggle;
    public Button player4Toggle;
    public Image player3Panel;
    public Image player4Panel;
    public bool player3Active;
    public bool player4Active;
    public Sprite toggleOff;
    public Sprite toggleOn;

    // Start is called before the first frame update
    void Start()
    {
        player3Active = false;
        player4Active = false;
        player3Panel.color = new Color(255, 255, 255, 0.5f);
        player4Panel.color = new Color(255, 255, 255, 0.5f);
        player3Toggle.image.sprite = toggleOff;
        player4Toggle.image.sprite = toggleOff;
        player4Toggle.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinuePressed()
    {
        if (player3Active && player4Active)
        {
            GameData.playerCount = 4;
        }
        else if (player3Active)
        {
            GameData.playerCount = 3;
        }
        else
        {
            GameData.playerCount = 2;
        }

        SceneManager.LoadScene("LevelSelectScene");
    }

    public void BackPressed()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void P3Toggled()
    {
        if (!player3Active)
        {
            // activated player 3
            player3Active = true;
            player3Panel.color = new Color(255, 255, 255, 1f);
            player3Toggle.image.sprite = toggleOn;
            player4Toggle.interactable = true;
        }
        else
        {
            // deactivated player 3
            player3Active = false;
            player4Active = false;
            player3Panel.color = new Color(255, 255, 255, 0.5f);
            player4Panel.color = new Color(255, 255, 255, 0.5f);
            player3Toggle.image.sprite = toggleOff;
            player4Toggle.image.sprite = toggleOff;
            player4Toggle.interactable = false;
        }
    }

    public void P4Toggled()
    {
        if (!player4Active)
        {
            // activated player 4
            player4Active = true;
            player4Panel.color = new Color(255, 255, 255, 1);
            player4Toggle.image.sprite = toggleOn;
        }
        else
        {
            player4Active = false;
            player4Panel.color = new Color(255, 255, 255, 0.5f);
            player4Toggle.image.sprite = toggleOff;
        }
    }
}
