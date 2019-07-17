using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour {
    public Rarity[] rarity;
    public bool respawnable;

    [Serializable]
    public class Rarity
    {
        public int rarity;
        public Tile.Scarcity scarcity;
    }
}
