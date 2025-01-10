using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player3;
    public GameObject player4;

    // Start is called before the first frame update
    void Start()
    {
        player3.SetActive(false);
        player4.SetActive(false);

        if (GameData.playerCount == 3)
        {
            player3.SetActive(true);
        }

        if (GameData.playerCount == 4)
        {
            player3.SetActive(true);
            player4.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
