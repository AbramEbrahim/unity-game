
using UnityEngine;

public class VoidTrigger : MonoBehaviour
{
    public Transform respawnPoint; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            RespawnPlayer(other.transform);
        }
    }

    void RespawnPlayer(Transform playerTransform)
    {
        playerTransform.position = respawnPoint.position;
        
    }
}
