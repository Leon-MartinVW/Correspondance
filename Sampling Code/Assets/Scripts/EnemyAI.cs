using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
//this script has an error cant find it
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed=200f;
    public float nextWayPointDistance=3f;

    Path path;
    int currentWayPoint =0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
       

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Debug.Log(reachedEndOfPath);
        Debug.Log(currentWayPoint);
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;//normalized is unit vector
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        //doesn't look like enemy is working with the direction and the force vectors

        Debug.Log(direction);
        Debug.Log(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
}
