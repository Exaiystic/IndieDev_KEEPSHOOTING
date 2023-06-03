using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    public int startHealth;
    public Color hitColour;
    public float hitTime;

    private int currentHealth;
    private SpriteRenderer sr;
    private Color startColour;

    private void Start()
    {
        currentHealth = startHealth;
        sr = GetComponent<SpriteRenderer>();
        startColour = sr.color;
    }

    public void TakeDamage()
    {
        currentHealth --;
        StartCoroutine(Hit());
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual IEnumerator Hit()
    {
        sr.color = hitColour;
        yield return new WaitForSeconds(hitTime);
        sr.color = startColour;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
