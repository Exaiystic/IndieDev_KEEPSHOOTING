using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public float offset; //Just in case the weapon sprite needs to be aligned
    public GameObject projectile;
    public GameObject muzzleFlash;
    public float flashTime;
    public Transform firePoint;
    public float fireRate;
    public int ammoCapacity;
    public int currentAmmo;
    public float reloadSpeed;
    public GameObject reloadBar;
    public float inaccuracy;
    public int projectileCount;
    public AudioSource gunShot;
    public AudioSource[] reloadNoise;

    private Vector3 dir;
    private GameObject crosshair;
    private float isShootHeld;
    private InputActions inputActions;
    private Camera main;
    private float timeBetweenShots;
    private ReloadManager rm;
    private SpriteRenderer sr;

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

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        inputActions.Player.Reload.performed += _ => StartCoroutine(Reload());
        main = Camera.main;
        currentAmmo = ammoCapacity;
        muzzleFlash.SetActive(false);
        crosshair = GameObject.Find("Crosshair");
        rm = reloadBar.GetComponent<ReloadManager>();
    }

    void Update()
    {
        isShootHeld = inputActions.Player.Shoot.ReadValue<float>();

        dir = crosshair.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (isShootHeld > 0)
        {
            if (timeBetweenShots <= 0)
            {
                if (currentAmmo == 0)
                {
                    StartCoroutine(Reload());
                }
                else
                {
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float firingError = Random.Range(inaccuracy, -inaccuracy);
                        firePoint.Rotate(0, 0, firingError);
                        Instantiate(projectile, firePoint.position, firePoint.rotation);
                        gunShot.Play();
                        firePoint.Rotate(0, 0, -firingError);
                    }
                    StartCoroutine(ShowMuzzleFlash());
                    currentAmmo--;
                    timeBetweenShots = fireRate;
                }
            }
        }
    }

    protected virtual IEnumerator Reload()
    {
        rm.duration = reloadSpeed;
        rm.SetisReloadingToTrue();
        yield return new WaitForSeconds(reloadSpeed);

        currentAmmo = ammoCapacity;
        int chosenSound = Random.Range(0, reloadNoise.Length);
        reloadNoise[chosenSound].Play();
    }

    protected virtual IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        muzzleFlash.SetActive(false);
    }
}
