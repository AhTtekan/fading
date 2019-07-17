using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Stalker : Enemy {
    
	// Use this for initialization
	void Start ()
    {
        wanderDestination = (Random.insideUnitSphere * 15) + transform.position;
        wanderDestination.z = 0;
        playerLc = GameObject.Find("Player").transform.Find("PlayerSprite").GetComponent<LightCheck>();
        timeUntilAutoAggro = Random.Range(60 * 60, 60 * 60 * 2);
        em = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
        timeSinceLastNoise = Random.Range(60 * 15, 60 * 120);
    }

    // Update is called once per frame
    void Update()
    {
        DoLoop();
        Debug.DrawLine(transform.position,
            wanderDestination, Color.yellow);
        em = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
    }

}
