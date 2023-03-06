using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    public Vector2 direction;
    public Rigidbody2D rigidbody2D;
    [Range(1.0f, 100.0f)]
    public float force;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerBehaviour>().transform;
        direction = Vector3.Normalize((player.position + ((offset.y <= player.position.y) ? offset : new Vector3(0.0f, 1.0f, 0.0f))) - transform.position);
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Activate()
    {
        Move();
        Invoke("DestroyYourself", 2.0f);
    }

    public void Move()
    {
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
    }


    private void Rotate()
    {
        transform.RotateAround(transform.position, Vector3.forward, 5.0f);
    }


    public void DestroyYourself()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyYourself();
        }
    }
}
