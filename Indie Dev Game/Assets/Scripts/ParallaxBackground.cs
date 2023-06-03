using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float effectStrength;
    public GameObject cam;

    private float length;
    private float startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - effectStrength));
        float dist = (cam.transform.position.x * effectStrength);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.x);

        /*Supposed to make the background loop, does not work. Little less optimal but a countermethod is to simply duplicate the backgrounds out to fill the whole level
        if (temp > startPos + length)
        {
            startPos += length;
        } else if (temp < startPos - length)
        {
            startPos -= length;
        }
        */
    }
}
