using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    GameObject player;
    float zoom;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        zoom = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoom += Input.GetAxis("Mouse ScrollWheel");
            if (zoom > -8)
                zoom = -8;
            if (zoom < -21)
                zoom = -21;
        }

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 5, zoom);
	}
}
