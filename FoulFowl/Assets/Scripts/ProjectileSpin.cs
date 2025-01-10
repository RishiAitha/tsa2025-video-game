using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpin : MonoBehaviour
{

    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
