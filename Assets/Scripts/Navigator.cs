using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour {

    public GameObject Orb;
    public GameObject Tower;
    TileGenerator tg;
	// Use this for initialization
	void Start ()
    {
        tg = GameObject.FindObjectOfType<TileGenerator>();
        var n1 = Resources.FindObjectsOfTypeAll<Pointer>(); //GameObject.FindObjectOfType<Pointer>();
        Pointer n = null;
        if (n1 != null)
            n = n1[0];
        n.Orb = GameObject.Instantiate(Orb, 
            tg.ChooseRandomLocation(), Orb.transform.rotation);
        n.Tower = GameObject.Instantiate(Tower, 
            tg.ChooseRandomLocation(), Tower.transform.rotation);
        n.gameObject.SetActive(true);
        var dm = GameObject.FindObjectOfType<DialogManager>();
        dm.queue.Enqueue(dm.dialogs[6]);
    }
}
