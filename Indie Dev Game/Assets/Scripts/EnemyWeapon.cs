using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float attackRange;
    public float attackRate;
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject projectile;
    public GameObject muzzleFlash;
    public GameObject muzzleFlash2;
    public float flashTime;
    public float inaccuracy;
    public AudioSource gunShotSound;

    private GameObject player;
    private Vector3 dir;
    private float distanceToPlayer;
    private float timeBetweenShots;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        muzzleFlash.SetActive(false);
        if (muzzleFlash2 != null)
        {
            muzzleFlash2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            dir = player.transform.position - transform.position; //Gets difference between player and enemy
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //Turns vector into an angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotates enemy based off angle

            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < attackRange)
            {
                Attack();
            }

            if (timeBetweenShots > 0)
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }

    private void Attack()
    {
        //Debug.Log("I am attacking");
        if (timeBetweenShots <= 0)
        {
            /* Inaccuracy durrently doesn't work, needs a fix */
            float firingError = Random.Range(inaccuracy, -inaccuracy);
            gunShotSound.Play();
            firePoint.Rotate(0, 0, firingError);
            Instantiate(projectile, firePoint.position, transform.rotation);
            firePoint.Rotate(0, 0, -firingError);
            if (firePoint2 != null)
            {
                firingError = Random.Range(inaccuracy, -inaccuracy);
                firePoint2.Rotate(0, 0, firingError);
                Instantiate(projectile, firePoint2.position, transform.rotation);
                firePoint2.Rotate(0, 0, -firingError);
            }
            StartCoroutine(ShowMuzzleFlash());
            timeBetweenShots = attackRate;
        }
    }

    protected virtual IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        if (muzzleFlash2 != null)
        {
            muzzleFlash2.SetActive(true);
        }
        yield return new WaitForSeconds(flashTime);
        muzzleFlash.SetActive(false);
        if (muzzleFlash2 != null)
        {
            muzzleFlash2.SetActive(false);
        }
    }
}
