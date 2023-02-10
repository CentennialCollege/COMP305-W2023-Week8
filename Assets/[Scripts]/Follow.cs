using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(0.0f, 0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(target.position.x, target.position.y) + offset;
    }
}
