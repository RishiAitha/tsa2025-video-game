using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public Image bossBar;
    public Image playerBar1;
    public Image playerBar2;
    public Image playerBar3;
    public Image playerBar4;
    public Image playerBarBorder3;
    public Image playerBarBorder4;

    public Image eggBar1;
    public Image eggBar2;
    public Image eggBar3;
    public Image eggBar4;
    public Image eggBarBorder3;
    public Image eggBarBorder4;

    public float startingPlayerHealth;
    public float startingBossHealth;

    // Start is called before the first frame update
    void Start()
    {
        startingPlayerHealth = player1.GetComponent<BirdController>().health;
        startingBossHealth = boss.GetComponent<BossController>().health;

        if (GameData.playerCount == 2)
        {
            eggBar3.color = new Color(1, 1, 1, 0);
            playerBar3.color = new Color(1, 1, 1, 0);
            eggBar4.color = new Color(1, 1, 1, 0);
            playerBar4.color = new Color(1, 1, 1, 0);
            eggBarBorder3.color = new Color(1, 1, 1, 0);
            eggBarBorder4.color = new Color(1, 1, 1, 0);
            playerBarBorder3.color = new Color(1, 1, 1, 0);
            playerBarBorder4.color = new Color(1, 1, 1, 0);
        }
        else if (GameData.playerCount == 3)
        {
            eggBar4.color = new Color(1, 1, 1, 0);
            playerBar4.color = new Color(1, 1, 1, 0);
            eggBarBorder4.color = new Color(1, 1, 1, 0);
            playerBarBorder4.color = new Color(1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossBar.fillAmount = boss.GetComponent<BossController>().health / startingBossHealth;
        playerBar1.fillAmount = player1.GetComponent<BirdController>().health / startingPlayerHealth;
        playerBar2.fillAmount = player2.GetComponent<BirdController>().health / startingPlayerHealth;
        if (player3.activeInHierarchy)
        {
            playerBar3.fillAmount = player3.GetComponent<BirdController>().health / startingPlayerHealth;
        }
        if (player4.activeInHierarchy)
        {
            playerBar4.fillAmount = player4.GetComponent<BirdController>().health / startingPlayerHealth;
        }
    }
}
