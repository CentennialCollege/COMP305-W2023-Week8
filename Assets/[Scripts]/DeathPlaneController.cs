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
            var player = other.gameObject.GetComponent<PlayerBehaviour>();
            player.life.LoseLife();
            player.health.ResetHealth();

            if (player.life.value > 0)
            {
                soundManager.PlaySoundFX(Channel.PLAYER_DEATH_FX, SoundFX.DEATH);
                ReSpawn(other.gameObject);
            }
        }
    }

    public void ReSpawn(GameObject go)
    {
        go.transform.position = currentSpawnPoint.position;
    }
}
