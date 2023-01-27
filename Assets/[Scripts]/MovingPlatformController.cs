using System.Collections;
using System.Collections.Generic;using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PathNode
{
    public Vector2 position;
    public PathNode next;
    public PathNode prev;

    // constructor
    public PathNode(Vector2 position, PathNode next, PathNode prev)
    {
        this.position = position;
        this.next = next;
        this.prev = prev;
    }
}

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement Properties")] 
    [Range(1.0f, 20.0f)]
    public float maxSpeed;
    [Range(0.01f, 0.2f)]
    public float timingValue = 0.02f;
    public bool timerIsActive;
    public float timer;
    public bool isLooping;
    public bool isReverse;

    [Header("Platform Path Points")]
    public List<PathNode> pathNodes;

    private Vector2 startPoint;
    private Vector2 endPoint;
    
    private PathNode currentNode;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        timerIsActive = true;
        isLooping = true;
        isReverse = false;

        startPoint = transform.position;
        BuildPathNodes();
    }

    private void BuildPathNodes()
    {
        // Creates a new Empty List Container
        pathNodes = new List<PathNode>();

        // adds all PathNodes to the pathNodes List
        foreach (Transform child in transform)
        {
            var pathNode = new PathNode(child.position, null, null);
            pathNodes.Add(pathNode);
        }

        // set up all links
        for (var i = 0; i < pathNodes.Count; i++)
        {
            pathNodes[i].next = (i == pathNodes.Count - 1) ? pathNodes[0] : pathNodes[i + 1];
            pathNodes[i].prev = (i == 0) ? pathNodes[^1] : pathNodes[i - 1];
        }

        currentNode = pathNodes[0];

        endPoint = currentNode.position;
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        if (timerIsActive)
        {
            if (timer <= 1.0f)
            {
                timer += timingValue;
            }

            if (timer >= 1.0f)
            {
                timer = 0.0f;

                // moving down the list
                startPoint = currentNode.position;

                if (!isReverse)
                {
                    endPoint = currentNode.next.position;

                    if (currentNode != pathNodes[^1])
                    {
                        currentNode = currentNode.next;
                    }
                    else if ((currentNode == pathNodes[^1]) && (isLooping))
                    {
                        currentNode = currentNode.next;
                    }
                    else
                    {
                        timerIsActive = false;
                    }
                }
                else
                {
                    endPoint = currentNode.prev.position;

                    if (currentNode != pathNodes[0])
                    {
                        currentNode = currentNode.prev;
                    }
                    else if ((currentNode == pathNodes[0]) && (isLooping))
                    {
                        currentNode = currentNode.prev;
                    }
                    else
                    {
                        timerIsActive = false;
                    }
                }
               
            }
        }
    }

    private void Move()
    {
        transform.position = Vector2.Lerp(startPoint, endPoint, timer);
    }
}
