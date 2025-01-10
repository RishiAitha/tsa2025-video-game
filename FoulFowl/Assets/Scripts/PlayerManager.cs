using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerList;
    public float[] playerDamageList = new float[4];

    public GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerDamageList = new float[4];

        playerList[2].SetActive(false);
        playerList[3].SetActive(false);

        if (GameData.playerCount == 3)
        {
            playerList[2].SetActive(true);
        }

        if (GameData.playerCount == 4)
        {
            playerList[2].SetActive(true);
            playerList[3].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerList[0].activeInHierarchy && !playerList[1].activeInHierarchy && !playerList[2].activeInHierarchy && !playerList[3].activeInHierarchy)
        {
            Time.timeScale = 0;
            loseMenu.SetActive(true);
        }
    }
}
