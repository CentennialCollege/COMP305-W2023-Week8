using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [Header("Movement Properties")]
    [Range(0.1f, 10.0f)]
    public float verticalSpeed;
    [Range(0.1f, 10.0f)]
    public float verticalDistance;

    private Vector2 startPoint;

    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPoint.x,
            Mathf.PingPong(Time.time * verticalSpeed, verticalDistance) + startPoint.y, 0.0f);
    }
}
