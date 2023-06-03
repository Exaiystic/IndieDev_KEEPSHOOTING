using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunManager : MonoBehaviour
{
    public int totalWeapons = 3;
    public GameObject weaponHolder;

    private InputActions inputActions;
    private int currentWeaponIndex;
    private GameObject[] guns;
    private GameObject currentGun;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputActions.Player.FirstWeapon.performed += _ => SwitchWeapon(0);
        inputActions.Player.SecondWeapon.performed += _ => SwitchWeapon(1);
        inputActions.Player.ThirdWeapon.performed += _ => SwitchWeapon(2);

        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchWeapon(int weapon)
    {
        guns[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = weapon;
        guns[currentWeaponIndex].SetActive(true);
    }
}
