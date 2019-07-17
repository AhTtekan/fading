using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Pickup : MonoBehaviour {
    
    public bool isCraftable = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "playerSprite")
    //    {
    //        ShowPickupDialog();
    //        isPickupable = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "playerSprite")
    //    {
    //        ClosePickupDialog();
    //        isPickupable = false;
    //    }
    //}

    public void ShowPickupDialog()
    {
        transform.Find("Pickup Button").gameObject.SetActive(true);
    }

    public void ClosePickupDialog()
    {
        transform.Find("Pickup Button").gameObject.SetActive(false);
    }

    public void PickupFailure()
    {
        //play fail sound
        StartCoroutine(Shake());
    }

    public void PickupSuccess()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player").transform.Find("Item Slot").gameObject;
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.SetParent(go.transform, false);
        GetComponent<CircleCollider2D>().enabled = false;
        //isPickupable = false;
        ClosePickupDialog();
    }
    public void DropSuccess()
    {
        transform.SetParent(null);
        GetComponent<CircleCollider2D>().enabled = true;
        //isPickupable = true;
        ShowPickupDialog();
    }
    IEnumerator Shake()
    {
        int length = 30;
        while(length > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-20, 20))), 
                Time.deltaTime * 10);
            length--;
            yield return null;
        }
        transform.rotation = new Quaternion();
        yield return null;
    }

    public void SetCraftable(bool input)
    {
        isCraftable = input;
    }
    public void SetPickupable(bool input)
    {
        //isPickupable = input;
        if (input)
            ShowPickupDialog();
        else
            ClosePickupDialog();
    }
}
