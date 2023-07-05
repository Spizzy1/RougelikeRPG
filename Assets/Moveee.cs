using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveee : MonoBehaviour
{
    bool invert;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x > 10 || gameObject.transform.position.x < -10)
        {
            invert = !invert;
        }
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + (Convert.ToInt32(invert) - (Convert.ToInt32(!invert))) * Time.deltaTime * 5, gameObject.transform.position.y);
    }
}
