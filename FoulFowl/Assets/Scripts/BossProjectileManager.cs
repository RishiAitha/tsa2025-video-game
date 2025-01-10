using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileManager : MonoBehaviour
{
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= 0.33)
        {
            GetComponent<SpriteRenderer>().sprite = sprite0;
        }
        else if (rand <= 0.67)
        {
            GetComponent<SpriteRenderer>().sprite = sprite1;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprite2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
