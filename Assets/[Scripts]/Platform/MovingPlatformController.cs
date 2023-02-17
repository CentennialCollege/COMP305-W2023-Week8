using System.Collections;
using System.Collections.Generic;using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;


public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement Properties")] 
    [Range(0.01f, 0.2f)]
    public float speedValue = 0.02f;
    public bool timerIsActive;
    public bool isLooping;
    public bool isReverse;

    [Header("Platform Path Points")]
    private List<PathNode> pathNodes;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private PathNode currentNode;
    private float timer;


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
                timer += speedValue;
            }

            if (timer >= 1.0f)
            {
                timer = 0.0f;

                Traverse((isReverse) ? 0 : ^1, (isReverse) ? currentNode.prev : currentNode.next);
            }
        }
    }

    private void Traverse(System.Index boundaryIndex, PathNode nextNode)
    {
        startPoint = currentNode.position;
        endPoint = nextNode.position;

        if (currentNode != pathNodes[boundaryIndex])
        {
            currentNode = nextNode;
        }
        else if ((currentNode == pathNodes[boundaryIndex]) && (isLooping))
        {
            currentNode = nextNode;
        }
        else
        {
            timerIsActive = false;
        }
    }

    private void Move()
    {
        transform.position = Vector2.Lerp(startPoint, endPoint, timer);
    }
}
