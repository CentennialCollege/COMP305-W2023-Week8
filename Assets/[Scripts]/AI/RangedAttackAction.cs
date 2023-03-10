using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAction : MonoBehaviour, Action
{
    public bool hasLOS;
    [Range(1, 100)] 
    public int fireDelay = 20;
    public Transform bulletSpawn;

    public GameObject bulletPrefab;
    public Transform bulletParent;

    void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("[BULLETS]").transform;
    }

    void Update()
    {
        hasLOS = GetComponent<PlayerDetection>().LOS;
    }

    void FixedUpdate()
    {
        if (hasLOS && Time.frameCount % fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
    }
}
