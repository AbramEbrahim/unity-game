using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCompat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public Transform jumpingAttackPoint;
    public float attackRange = 0.5f;
    int damage = 20;
    [SerializeField] AudioClip attackSound;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    private Rigidbody2D rb;

    // Offset from character's position to attack point
    public Vector3 attackPointOffset = new Vector3(1f, 0f, 0f); // Adjust as needed

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SoundManager.instance.PlaySound(attackSound);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SoundManager.instance.PlaySound(attackSound);
                JumpingAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        
        Vector3 attackPos = transform.position + transform.right * attackPointOffset.x;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);         
        }
    }

    void JumpingAttack()
    {
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        animator.SetTrigger("JumpingAttack");

       

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(jumpingAttackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(transform.position + transform.right * attackPointOffset.x, attackRange);
    }
}
