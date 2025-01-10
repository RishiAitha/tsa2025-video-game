using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWarp : MonoBehaviour
{


    void Start()
    {
    }

    // Update is called once per frame
    void Update()   
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        float rightScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;

        float topScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float bottomScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).y;

        if (screenPos.x <= 0) {
            transform.position = new Vector2(rightScreenInWorld, transform.position.y);
        }else if (screenPos.x >= Screen.width){
            transform.position = new Vector2(leftScreenInWorld, transform.position.y);    
        }else if (screenPos.y >= Screen.height){
            transform.position = new Vector2(transform.position.x, bottomScreenInWorld);
        }else if (screenPos.y <= 0){
            transform.position = new Vector2(transform.position.x, topScreenInWorld);
        }
    }
}
