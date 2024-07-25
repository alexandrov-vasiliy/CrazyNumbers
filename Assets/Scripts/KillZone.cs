using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseObstacle baseObstacle;
        
        if (other.gameObject.TryGetComponent<BaseObstacle>(out baseObstacle))
        {
            other.gameObject.SetActive(false);
        }

    }
}