
using System.Xml.Serialization;
using UnityEngine;

public class spikesHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

    [SerializeField] private AudioClip impactSound;
    private void OnEnable()
    {
        Stop(); 
    }
    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);

        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirection();
        for (int i = 0; i < directions.Length; i++) 
        {   
            Debug.DrawRay(transform.position, directions[i],Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position,directions[i],range,PlayerLayer);
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;

    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
