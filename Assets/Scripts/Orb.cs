using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    bool dialog = false;
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 12 && !dialog)
        {
            dialog = true;
            var dm = GameObject.FindObjectOfType<DialogManager>();
            dm.queue.Enqueue(dm.dialogs[7]);
        }
    }
}
