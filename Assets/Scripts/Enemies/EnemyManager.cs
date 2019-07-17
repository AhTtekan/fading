using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Cave[] caves;
    [SerializeField]
    public List<Enemy> enemies;
    public GameObject player;
    public int EnemyCap;
    bool brick = false;
	// Update is called once per frame
	void Update ()
    {
        if (!brick)
        {
            Maintain();
        }
	}
    public void Win()
    {
        brick = true;
    }

    private void Maintain()
    {
        if(enemies.Count < EnemyCap)
        {
            SpawnNew();
        }
    }

    private void SpawnNew()
    {
        Cave closest = null;
        float dist = float.MaxValue, current=0f;
        for (int i = 0; i < caves.Length; i++)
        {
            current = Vector3.Distance(player.transform.position, caves[i].transform.position);
            if (current < dist)
            {
                dist = current;
                closest = caves[i];
            }
        }
        enemies.Add(closest.SpawnEnemy());
    }

    public void Initialize()
    {
        caves = GameObject.FindObjectsOfType<Cave>();
        //spawn initial enemies
        foreach(var cave in caves)
        {
            var dist = Vector3.Distance(player.transform.position, cave.transform.position);
            if (dist <= 300 && enemies.Count < EnemyCap)
            {
                for (int i = 0; i < Random.Range(1, 3); i++)
                {
                    enemies.Add(cave.SpawnEnemy());
                }
            }
        }
    }
}
