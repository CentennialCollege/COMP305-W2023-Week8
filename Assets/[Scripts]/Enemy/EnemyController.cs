using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Properties")]
    public float horizontalSpeed = 1.0f;
    public Transform inFrontCheck;
    public Transform groundAheadCheck;
    public Transform groundPoint;
    public float groundRadius;
    public LayerMask groundLayerMask;
    public bool isObstacleInFront;
    public bool isGroundAhead;
    public bool isGrounded;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        isObstacleInFront = Physics2D.Linecast(groundPoint.position, inFrontCheck.position, groundLayerMask);
        isGroundAhead = Physics2D.Linecast(groundPoint.position, groundAheadCheck.position, groundLayerMask);
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayerMask);

        if (isGroundAhead)
        {
            Move();
        }

        if (isObstacleInFront || !isGroundAhead)
        {
            Flip();
        }
    }

    void Move()
    {
        transform.position += new Vector3(direction.x * horizontalSpeed * Time.deltaTime, 0.0f);
    }

    void Flip()
    {
        var x = transform.localScale.x * -1.0f;
        direction *= -1.0f;
        transform.localScale = new Vector3(x, 1.0f, 1.0f);
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
        Gizmos.DrawLine(groundPoint.position, inFrontCheck.position);
        Gizmos.DrawLine(groundPoint.position, groundAheadCheck.position);
    }
}
