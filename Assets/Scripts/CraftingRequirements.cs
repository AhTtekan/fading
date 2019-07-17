using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirements : MonoBehaviour {

    public bool open = false;
    private int xVal = 70;
    RectTransform tran;
    [SerializeField]
    float speed;
    private void Start()
    {
        tran = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update () {
        if (open)
        {
            xVal = -100;
        }
        else
        {
            xVal = 70;
        }
        if(tran.anchoredPosition.x != xVal)
        {
            tran.anchoredPosition =
                Vector3.MoveTowards(tran.anchoredPosition,
                new Vector3(xVal, tran.anchoredPosition.y), Time.deltaTime * speed);
        }
	}
}
