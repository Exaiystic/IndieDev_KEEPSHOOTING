using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float startTimeBetweenSpawns;
    public GameObject echo;

    private float timeBetweenSpawns;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (timeBetweenSpawns <= 0)
        {
            GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 1f);
            timeBetweenSpawns = startTimeBetweenSpawns;
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
