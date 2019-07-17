using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog")]
public class Dialog : ScriptableObject {

    public bool repeatable;
    public string[] texts;
    public float timeSinceRepeat;
    public bool shown;
    
}
