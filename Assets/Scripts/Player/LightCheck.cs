using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour {

    public bool overrideCheck =false;
    [SerializeField]
    public bool isInLight
    {
        get
        {
            if (overrideCheck)
                return true;
            return lightFromInv || lightFromWorld;
        }
    }
    [SerializeField]
    bool lightFromInv, lightFromWorld;
    public Transform lightSource;
    [SerializeField]
    Inventory inventory;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(inventory != null)
        {
            if (inventory.ActiveItem != null && inventory.ActiveItem.name.ToLower().Contains("torch"))
                lightFromInv = true;
            else
                lightFromInv = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "light")
        {
            lightFromWorld = true;
            lightSource = collision.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "light")
        {
            lightFromWorld = false;
            lightSource = null;
        }
    }
}
