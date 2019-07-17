using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Working : MonoBehaviour {

    public PlayerAnimations pa;
    public PlayerMovement pm;
    public GameManager gm;
    public List<IWorkable> inRangeWorkables;
    public IWorkable closestWorkable;
	// Use this for initialization
	void Start () {
        inRangeWorkables = new List<IWorkable>();
        StartCoroutine(CheckRange());
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Work") != 0)// && gm.IsClosestWorkable(this.transform))
        {
            pm.playerCanMove = false;
            //var workable = gm.GetClosestWorkable(); //GameObject.FindObjectsOfType<CraftPile>().FirstOrDefault(x => x.isWorkable);
            if (closestWorkable != null)
            {
                closestWorkable.Work();
                pa.SetBool("Crouching", true);
            }
            else
            {
                pa.SetBool("Crouching", false);
            }
        }
        else
        {
            pa.SetBool("Crouching", false);
        }
    }
    IEnumerator CheckRange()
    {
        while (true)
        {
            //unsure why this keeps ending up null???
            if (inRangeWorkables == null)
                inRangeWorkables = new List<IWorkable>();
            if (inRangeWorkables.Count == 0)
                closestWorkable = null;

            float closest = 100f;
            MonoBehaviour g;
            foreach (var i in inRangeWorkables)
            {
                g = i as MonoBehaviour;
                var newD = Vector3.Distance(transform.position,
                    g.transform.position);
                if (newD < closest)
                {
                    closest = newD;
                    closestWorkable = i;
                }
                i.SetWorkable(false);
            }
            if (closestWorkable != null)
                closestWorkable.SetWorkable(true);
            yield return null;
        }
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var w = collision.GetComponent<IWorkable>();
        if (w != null && !inRangeWorkables.Contains(w))
        {
            inRangeWorkables.Add(w);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var w = collision.GetComponent<IWorkable>();
        if (w != null)
        {
            if (closestWorkable == this)
                closestWorkable = null;
            w.SetWorkable(false);
            inRangeWorkables.Remove(w);
        }
    }
}
