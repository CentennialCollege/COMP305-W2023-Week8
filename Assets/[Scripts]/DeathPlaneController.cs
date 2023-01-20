using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform currentSpawnPoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ReSpawn(other.gameObject);
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = currentSpawnPoint.position;
    }
}
