using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Furnace : MonoBehaviour, IWorkable {

    [SerializeField]
    Sprite unlitFurnace;
    public int MaxUses;
    private int uses = 0;
    public Smeltable addedItem;
    [SerializeField]
    float currentSmeltTime = 0;
	// Use this for initialization
	void Start () {

        var dm = GameObject.FindObjectOfType<DialogManager>();
        dm.queue.Enqueue(dm.dialogs[4]);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(addedItem != null && uses < MaxUses)
        {
            currentSmeltTime += Time.deltaTime;
            transform.Find("Percent").transform.localScale =
                new Vector3(
                    currentSmeltTime / addedItem.timeToSmelt,
                    1);

            if (currentSmeltTime >= addedItem.timeToSmelt)
            {
                FinishSmelting();
            }
        }
        else
            currentSmeltTime = 0;
    }

    private void FinishSmelting()
    {
        uses++;
        if(uses >= MaxUses)
            PutOut();
        Vector3 randomFactor = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GameObject.Instantiate(addedItem.result, transform.position + randomFactor, transform.rotation, null);
        GameObject.Destroy(addedItem.gameObject);
        addedItem = null;
        transform.Find("Percent").transform.localScale =
            new Vector3(0, 1);
    }

    private void PutOut()
    {
        var dm = GameObject.FindObjectOfType<DialogManager>();
        dm.queue.Enqueue(dm.dialogs[5]);
        GetComponent<SpriteRenderer>().sprite = unlitFurnace;
        //TODO: Disable flames, essentially makes this useless except to craft a new furnace
    }

    public void Work()
    {
        if(addedItem != null || uses >= MaxUses)
        {
            AddItemFail();
            return;
        }
        Inventory i = GameObject.FindObjectOfType<Inventory>();
        if (i.ActiveItem == null)
        {
            AddItemFail();
            return;
        }
        Smeltable s = i.ActiveItem.GetComponent<Smeltable>();
        if(s == null)
        {
            AddItemFail();
            return;
        }
        AddItem(i);
    }

    private void AddItem(Inventory i)
    {
        addedItem = i.ActiveItem.GetComponent<Smeltable>();
        addedItem.gameObject.SetActive(false);
        i.RemoveActiveItem();
    }
    

    private void AddItemFail()
    {
        //play sound,
        //shake furnace
        //dialog
    }

    public void SetWorkable(bool input)
    {
        if(addedItem != null)
        {
            HideWorkDialog();
            return;
        }
        if (input)
        {
            ShowWorkDialog();
        }
        else
        {
            HideWorkDialog();
        }
    }

    public void ShowWorkDialog()
    {
        transform.Find("Work Button").gameObject.SetActive(true);
    }

    public void HideWorkDialog()
    {
        transform.Find("Work Button").gameObject.SetActive(false);
    }
}
