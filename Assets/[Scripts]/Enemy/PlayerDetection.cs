using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sensing Suite")] 
    public LayerMask collisionLayerMask;
    public bool playerDetected;
    public bool LOS;

    public float playerDirection;
    public float enemyDirection;
    public Vector2 playerDirectionVector;

    public Collider2D colliderName;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerDirectionVector = Vector2.zero;
        playerDirection = 0;
        enemyDirection = 0;

        playerTransform = FindObjectOfType<PlayerBehaviour>().transform;
        playerDetected = false;
        LOS = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            var hit = Physics2D.Linecast(transform.position, playerTransform.position, collisionLayerMask);
            colliderName = hit.collider;

            playerDirectionVector = (playerTransform.position - transform.position); // entire vector = we only need a unit vector
            playerDirectionVector.Normalize(); // creates the unit vector (magnitude of 1)
            playerDirection = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;
            enemyDirection = GetComponent<EnemyController>().direction.x;

            LOS = (hit.collider.gameObject.name == "Player") && (playerDirection == enemyDirection);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerDetected = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = (LOS) ? Color.green : Color.red;

        if (playerDetected)
        {
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = (playerDetected) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 20.0f);
    }
}
