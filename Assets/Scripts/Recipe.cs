using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recipe", menuName ="Craftable")]
public class Recipe : ScriptableObject {

    public bool completed = false;
    [SerializeField]
    public Requirements[] ingredients;

    public Pickup result;

    [Serializable]
    public class Requirements
    {
        public Pickup pickup;
        public int amount;
    }
}
