using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    
    [SerializeField] private float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;
    private bool active;
    private void Awake()
    {
        animator = GetComponent<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        // turn red to notify 
        triggered = true;
        spriteRenderer.color = Color.red;


        // wait for delay then activate 
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);


        // wait until x seconds then deactivate 
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
