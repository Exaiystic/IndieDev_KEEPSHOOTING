using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Text uiElement;
    public GameObject[] weapons;

    private int playerAmmo;
    private GameObject weaponIndex;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                if (weapons[i].activeSelf)
                {
                    weaponIndex = weapons[i];
                    playerAmmo = weaponIndex.GetComponent<Weapon>().currentAmmo;
                }
            }
        }

        uiElement.text = playerAmmo.ToString();
    }
}
