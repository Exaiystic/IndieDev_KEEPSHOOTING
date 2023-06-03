using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private GameObject go;
    private int minDamage;
    private int maxDamage;
    private bool isEnemyBullet;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        if (gameObject.tag == "Enemy Bullet")
        {
            //Debug.Log("Bullet belongs to an enemy");
            isEnemyBullet = true;
        }
        else
        {
            //Debug.Log("Bullet belongs to a friendly");
            isEnemyBullet = false;
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isEnemyBullet)
            {
                Player player = collision.GetComponent<Player>();
                player.TakeDamage();

                DestroyProjectile();
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (isEnemyBullet == false)
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.TakeDamage();

                //Will be more efficient to call the hurt func of the collided object instead of checking if the object was a friendly or enemy.
                //Maybe with interfaces? Something to look into later
                DestroyProjectile();
            }
        }
        else if (collision.gameObject.tag == "Environment")
        {
            DestroyProjectile();
        }

        void DestroyProjectile()
        {
            //Add bullet dissipate effect here
            Destroy(gameObject);
        }
    }
}
