using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBool(string boolName,bool input)
    {
        bool val = anim.GetBool(boolName);
        if (val != input)
            anim.SetBool(boolName, input);
    }
}
