using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameManager gm;
    Pickup[] items = new Pickup[4];
    [SerializeField]
    Image[] itemImages;
    int activeIndex;
    Pickup closestPickup;

    public List<Pickup> inRangePickups;
    
    //public bool submitPressed = false;
	// Use this for initialization
	void Start () {
        SetActiveItem(0);
        inRangePickups = new List<Pickup>();
        StartCoroutine(CheckRange());
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        foreach(Transform t in transform)
        {
            t.localPosition = new Vector3();
        }

        if (Input.GetAxis("Submit") > 0)
        {
            if (!gm.IsKeyPressed)
            {
                if(closestPickup != null)
                    PickUp(closestPickup);
                //var GOs = GameObject.FindObjectsOfType<Pickup>().Where(x => x.isPickupable);
                //if (GOs.Any())
                //{
                //    PickUp(GOs.Where(x => x.isPickupable).First());
                //}
                gm.IsKeyPressed = true;
            }
        }
        else if (Input.GetAxis("Cancel") > 0)
        {
            if (!gm.IsKeyPressed)
            {
                if (items[activeIndex] != null)
                {
                    Drop();
                }
                gm.IsKeyPressed = true;
            }
        }
        else if (Input.GetAxis("ItemSelect") != 0)
        {
            if (!gm.IsKeyPressed)
            {
                if (Input.GetAxisRaw("ItemSelect") > 0)
                {
                    activeIndex++;
                    if (activeIndex > 3)
                        activeIndex = 0;
                }
                else
                {
                    activeIndex--;
                    if (activeIndex < 0)
                        activeIndex = 3;
                }
                //seems redundant, but maybe something else procs SetActiveItem later
                SetActiveItem(activeIndex);
                gm.IsKeyPressed = true;
            }
        }
        
	}

    private void Drop()
    {
        items[activeIndex].DropSuccess();
        RemoveActiveItem();
    }

    private void SetActiveItem(int index)
    {
        activeIndex = index;
        Image im; Color co;
        for (int i = 0; i < 4; i++)
        {
            //GUI
            if(itemImages[i] != null)
            {
                im = itemImages[i].transform.parent.GetComponent<Image>();
                co = im.color;
                co.a = i == activeIndex ? 1f : 0.5f;
                im.color = co;
            }
            //ItemSlot
            if (items[i] != null)
            {
                if (i != activeIndex)
                {
                    items[i].gameObject.SetActive(false);
                }
                else
                {
                    items[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void PickUp(Pickup item)
    {
        if(items[activeIndex] == null)
        {
            items[activeIndex] = item;
            itemImages[activeIndex].gameObject.SetActive(true);
            itemImages[activeIndex].sprite =
                item.GetComponent<SpriteRenderer>().sprite;
            item.PickupSuccess();
        }
        else
        {
            item.PickupFailure();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pickup"))
        {
            if(!inRangePickups.Contains(collision.GetComponent<Pickup>()))
                inRangePickups.Add(collision.GetComponent<Pickup>());
        }
    }
    void Update()
    {

    }
    IEnumerator CheckRange()
    {
        while (true)
        {
            float closest = 100f;
            foreach (var i in inRangePickups)
            {
                var newD = Vector3.Distance(transform.position,
                    i.transform.position);
                if (newD < closest)
                {
                    closest = newD;
                    closestPickup = i;
                }
                //else
                i.SetPickupable(false);
            }

            if (closestPickup != null)
                closestPickup.SetPickupable(true);
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("pickup"))
        {
            var p = collision.GetComponent<Pickup>();
            //compare current pickupable item range w/ this one.
            p.SetPickupable(false);
            if (closestPickup == p)
                closestPickup = null;
            inRangePickups.Remove(p);
        }
    }

    public Pickup ActiveItem
    {
        get
        {
            return items[activeIndex];
        }
    }

    public void RemoveActiveItem()
    {
        itemImages[activeIndex].sprite = null;
        itemImages[activeIndex].gameObject.SetActive(false);
        //GameObject.Destroy(items[activeIndex].gameObject);
        items[activeIndex] = null;
    }
}
