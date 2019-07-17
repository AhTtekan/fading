using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPile : MonoBehaviour, IWorkable {
    
    public Pickup result;
    public float timeToCraft = 1f, speed = 20f;
    private float timeLeft;

    public IEnumerable<object> Where { get; internal set; }

    // Use this for initialization
    void Start () {
        timeLeft = timeToCraft;
	}
	
    public void Work()
    {

        timeLeft -= Time.deltaTime * speed;
        if (timeLeft <= 0)
        {
            GameObject.Instantiate(result, transform.position,transform.rotation,null);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            transform.Find("Percent").transform.localScale =
                new Vector3(
                    1 - (timeLeft / timeToCraft),
                    1);
        }
    }

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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "playerSprite")
    //    {
    //        isWorkable = true;
    //        ShowWorkDialog();
    //    }
    //}
    
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    if (collision.tag == "playerSprite")
    //    {
    //        isWorkable = false;
    //        HideWorkDialog();
    //    }
    //}
    

    public void ShowWorkDialog()
    {
        transform.Find("Work Button").gameObject.SetActive(true);
    }

    public void HideWorkDialog()
    {
        transform.Find("Work Button").gameObject.SetActive(false);
    }
}
