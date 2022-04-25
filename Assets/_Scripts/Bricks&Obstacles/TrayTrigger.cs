using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameEvents.Instance.ObstacleCollisionExit();
        }
    }
}
