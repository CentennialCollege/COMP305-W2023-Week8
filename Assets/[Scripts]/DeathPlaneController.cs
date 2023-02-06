using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform currentSpawnPoint;

    private SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ReSpawn(other.gameObject);

            soundManager.PlaySoundFX(Channel.PLAYER_DEATH_FX, SoundFX.DEATH);
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = currentSpawnPoint.position;
    }
}
