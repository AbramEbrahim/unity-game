using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth = 0;



    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage (int damage)
    {
    
        currentHealth -= damage;

        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetBool("die", true);
        GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero; 
        }
        this.enabled = false;
        StartCoroutine(DelayDestroy());

    }
    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2f); 
        Destroy(gameObject); 
    }
}
