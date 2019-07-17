using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Harvest", menuName = "Harvest")]
public class Harvest : ScriptableObject {

    public float timeToHarvest;
    [SerializeField]
    public HarvestOutput[] possibleOut;

    [Serializable]
    public class HarvestOutput
    {
        public Pickup pickup;
        public int chance;
    }
}
