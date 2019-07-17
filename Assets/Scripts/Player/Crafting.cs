using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Crafting : MonoBehaviour {

    private bool crafting = false, craftPressed = false;
    public GameManager gm;
    public PlayerMovement pm;
    [SerializeField]
    GameObject craftPile;
    [SerializeField]
    CraftingRequirements craftingRequirementsBox;
    [SerializeField]
    float radius;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Craft") > 0)
        {
            if (!gm.IsKeyPressed)
            {
                pm.playerCanMove = false;
                gm.IsKeyPressed = true;
                crafting = true;
                ShowCraftingCircle();
            }
        }
        else
        {
            crafting = false;
            craftPressed = false;
            HideCraftingCircle();

        }

        if (crafting)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            //var o = Mathf.Atan2(y, x);
            //var o2 = Mathf.Abs(Mathf.Rad2Deg * o);
            if (x != 0 || y != 0)
            {
                //Debug.DrawRay(transform.position, new Vector3(x, y) * o2, Color.blue);
                Debug.DrawLine(transform.position,
                    transform.position +
                    Vector3.Normalize(new Vector3(x, y)) * 2,
                    Color.green);// * o2), Color.green);

                RaycastHit2D hit = Physics2D.Raycast(transform.position,
                    Vector3.Normalize(new Vector3(x, y)), 2, 1 << 8);
                if(hit.collider != null && hit.collider.name == "SelectionBackground")
                {
                    foreach(CraftingSelection cs in transform.GetComponentsInChildren<CraftingSelection>())
                    {
                        cs.ResetSpriteColor();
                    }
                    var sr = hit.collider.GetComponent<SpriteRenderer>();
                    Color co = sr.color;
                    co = Color.red;
                    sr.color = co;
                    var c = hit.collider.transform.parent.GetComponent<CraftingSelection>();
                    if(c != null)
                        c.SelectForCrafting();//.selectedForCrafting = true;
                }
            }
            if (Input.GetAxis("CraftSelect") != 0 && !craftPressed)
            {
                craftPressed = true;
                var c = GameObject.FindObjectsOfType<CraftingSelection>().FirstOrDefault(a => a.selectedForCrafting);
                if (c != null)
                {
                    Pickup p = null;
                    List<Pickup> inRange = new List<Pickup>();
                    foreach(var o in Physics2D.OverlapCircleAll(transform.position, radius))
                    {
                        p = o.GetComponent<Pickup>();
                        if (p != null)
                            inRange.Add(p);
                    }
                    c.Craft(craftPile, inRange);
                }
            }
            else
            {
                craftPressed = false;
            }
        }
        else
        {
            craftingRequirementsBox.open = false;
        }
    }

    private void ShowCraftingCircle()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(true);
            t.GetComponent<CraftingSelection>().PushOut();
        }
    }
    private void HideCraftingCircle()
    {
        foreach (var t in transform.GetComponentsInChildren<CraftingSelection>())
        {
            t.PullIn();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Pickup obj = collision.GetComponent<Pickup>();
    //    if(obj != null)
    //    {
    //        obj.SetCraftable(true);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Pickup obj = collision.GetComponent<Pickup>();
    //    if (obj != null)
    //    {
    //        obj.SetCraftable(false);
    //    }
    //}


}
