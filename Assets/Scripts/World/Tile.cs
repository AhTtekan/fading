using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour {

    [SerializeField]
    ScarcitySpawnRate[] scarcityRate;
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    Spawnable[] populateObjs;
    [SerializeField]
    Scarcity scarcity;
    [HideInInspector]
    public int x, y;
    [SerializeField]
    int sections, respawnTime;
    int spawnTimeMin = 60 * 3, spawnTimeMax = 60 * 10;
    float Size
    {
        get
        {
            return sr.size.x * transform.localScale.x;
        }
    }
    float SectionSize
    {
        get
        {
            return sr.size.x / sections;
        }
    }
	
    IEnumerator Repopulate()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            //spawn an object somewhere in each section
            Populate(true);
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            respawnTime = UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }
    
    public Vector3 GetRandomLocation()
    {
        return GetRandomLocation(Random.Range(0, sections));
    }

    public Vector3 GetRandomLocation(int section)
    {
        int secX = section % sections;
        int secY = section / sections;
        Vector3 min = new Vector3(secX * SectionSize, secY * SectionSize);
        Vector3 max = new Vector3(min.x + SectionSize, min.y + SectionSize);

        return new Vector3(Random.Range(min.x, max.x) - 75,
                Random.Range(min.y, max.y) - 75);
    }

    void Populate(bool repopulate = false)
    {
        for (int i = 0; i < sections * sections; i++)
        {
            Populate(i, repopulate);
        }
    }

    void Populate(int section, bool repopulate)
    {

        //spawn objects
        if (repopulate)
        {
            GameObject spawnItem = GameObject.Instantiate(GetRespawnObj(), this.transform);
            spawnItem.transform.localPosition =
                GetRandomLocation(section);
        }
        else
        {
            //initial population
            foreach(var obj in populateObjs)
            {
                Spawnable.Rarity r = obj.rarity.FirstOrDefault(x => x.scarcity == scarcity);
                if (r != null && r.rarity != 0)
                {
                    int amount = r.rarity / 100;
                    for (int i = 0; i < 10; i++)
                    {
                        //Random.InitState(DateTime.Now.Millisecond);
                        if (Random.Range(0, 255) < r.rarity)
                            amount++;
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        var g = GameObject.Instantiate(obj, this.transform);
                        g.transform.localPosition =
                            GetRandomLocation(section);
                    }
                }
            }
        }
    }

    private GameObject GetRespawnObj()
    {
        int total = populateObjs.Sum(x => x.rarity
        .First(a => a.scarcity == scarcity).rarity);
        int current = 0;
        int chosen = Random.Range(0, total);
        GameObject output = null;
        for (int i = 0; i < populateObjs.Length; i++)
        {
            current += populateObjs[i].rarity.First(x => x.scarcity == scarcity).rarity;
            if(chosen < current)
            {
                output = populateObjs[i].gameObject;
                break;
            }
        }

        return output;
        //throw new NotImplementedException();
    }

    public void Initialize()
    {
        UnityEngine.Random.InitState(GetInstanceID());
        respawnTime = UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax);
        //Array values = Enum.GetValues(typeof(Scarcity));
        //scarcity = (Scarcity)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        int spawnRate = 0;
        int randVal = UnityEngine.Random.Range(1, 100);
        scarcity = Scarcity.Scarce;
        for (int i = 0; i < scarcityRate.Length; i++)
        {
            spawnRate += scarcityRate[i].rate;
            if(randVal <= spawnRate)
            {
                scarcity = scarcityRate[i].scarcity;
                break;
            }
        }
        Populate();
        StartCoroutine(Repopulate());
    }

    public enum Scarcity { Abundant, Filled, Scarce, Desolate, Rocky, Foresty }

    [Serializable]
    public class ScarcitySpawnRate
    {
        public Scarcity scarcity;
        public int rate;
    }
}
