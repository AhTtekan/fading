using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IWorkable {

    public Inventory inventory;
    bool dialog = false;
    public void SetWorkable(bool input)
    {
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
        transform.Find("Harvest Button").gameObject.SetActive(true);
    }

    public void HideWorkDialog()
    {
        transform.Find("Harvest Button").gameObject.SetActive(false);
    }

    public void Work()
    {
        if(inventory.ActiveItem != null)
        {
            if(inventory.ActiveItem.GetComponent<Orb>() != null)
            {
                GameObject.FindObjectOfType<GameManager>().Win();
            }
        }
    }

    // Use this for initialization
    void Start () {
        inventory = GameObject.FindObjectOfType<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(inventory.transform.position, transform.position);
        if(dist < 12 && !dialog)
        {
            dialog = true;
            var dm = GameObject.FindObjectOfType<DialogManager>();
            dm.queue.Enqueue(dm.dialogs[8]);
        }
	}
}
