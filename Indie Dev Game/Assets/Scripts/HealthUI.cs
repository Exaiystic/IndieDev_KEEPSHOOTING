using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;

    private GameObject playerObject;
    private Player playerScript;
    private int playerHealth;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<Player>();
    }

    void Update()
    {
        playerHealth = playerScript.GetHealth();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
