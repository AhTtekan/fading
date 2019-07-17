using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSelection : MonoBehaviour {
    bool isOpening = false, isClosing = false;
    public bool selectedForCrafting;
    public Recipe craftableItem;
    [SerializeField]
    Sprite unknownSprite;
    [SerializeField]
    float speed = 5;
    [SerializeField]
    CraftingRequirements craftingRequirementsBox;
    [SerializeField]
    Inventory inventory;
    private void Start()
    {
        craftableItem.completed = false;
        UpdateSprite();
    }
    private void Update()
    {

    }

    public void SelectForCrafting()
    {
        selectedForCrafting = true;

        ShowRequirementsBox();
    }

    private void ShowRequirementsBox()
    {
        //Set Sprites based on associated recipe
        int loop = 0;
        Sprite[] req = new Sprite[craftableItem.ingredients.Sum(x => x.amount)];
        for (int i = 0; i < craftableItem.ingredients.Length; i++)
        {
            for (int j = 0; j < craftableItem.ingredients[i].amount; j++)
            {
                req[loop] = craftableItem.ingredients[i].pickup
                    .GetComponent<SpriteRenderer>().sprite;
                    loop++;
            }
        }
        for (int i = 1; i <= 16; i++)
        {
            Image sr = craftingRequirementsBox.transform.Find("Image"+i)
                .GetComponent<Image>();
            if(req.Length < i)
            {
                sr.gameObject.SetActive(false);
                sr.sprite = null;
            }
            else
            {
                sr.gameObject.SetActive(true);
                sr.sprite = req[i - 1];
            }
        }
        //start coroutine to pull in
        craftingRequirementsBox.open = true;
    }

    private void OnEnable()
    {
        UpdateSprite();
        ResetSpriteColor();
    }
    
    public void ResetSpriteColor()
    {
        var sr = transform.Find("SelectionBackground").GetComponent<SpriteRenderer>();
        Color co = sr.color;
        co = Color.white;
        sr.color = co;
        selectedForCrafting = false;
    }

    public void Craft(GameObject craftPilePrefab, List<Pickup> inRange)
    {
        //find requirements in craftable range
        var craftableItems = GetCraftable(inRange);
        if (CheckRequirements(craftableItems))
        {
            ConsumeRequirements(inRange);
            //craftPilePrefab.GetComponent<CraftPile>().result = craftableItem.result;
            GameObject.Instantiate(craftPilePrefab, transform.position, 
                Quaternion.Euler(new Vector3(0,0,0)), null)
                .GetComponent<CraftPile>().result = craftableItem.result;
            craftableItem.completed = true; 
        }
        else
        {
            //play fail sound
        }
    }

    private Dictionary<string, int> GetCraftable(List<Pickup> inRange)
    {
        Dictionary<string, int> amounts = new Dictionary<string, int>();
        //foreach (Pickup p in GameObject.FindObjectsOfType<Pickup>().Where
        //    (x => x.isCraftable))
        //foreach (var p in inventory.inRangePickups)
        foreach(var p in inRange)
        {
            var name = p.GetComponent<SpriteRenderer>().sprite.name
                .ToLower()
                .Replace("1", "")
                .Replace("2", "")
                .Replace("3", "")
                .Replace("4", "")
                .Replace("5", "")
                .Replace("6", "")
                .Replace("7", "")
                .Replace("8", "")
                .Replace("9", "")
                .Replace("0", "");
            if (amounts.ContainsKey(name))
            {
                amounts[name] += 1;
            }
            else
            {
                amounts.Add(name, 1);
            }
        }
        return amounts;
    }

    private void ConsumeRequirements(List<Pickup> inRange)
    {
        Dictionary<string, int> items = new Dictionary<string, int>();
        foreach(var r in craftableItem.ingredients)
        {
            items.Add(r.pickup.name, r.amount);
        }

        //foreach (Pickup p in GameObject.FindObjectsOfType<Pickup>().Where
        //    (x => x.isCraftable))
        //foreach(var p in inventory.inRangePickups)
        foreach(var p in inRange)
        {
            var name = p.GetComponent<SpriteRenderer>().sprite.name;

            if (items.ContainsKey(name) && items[name] > 0)
            {
                items[name] -= 1;
                GameObject.Destroy(p.gameObject);
            }
            //break if all requirements met.
            if(items.Sum(y => y.Value) == 0) { return; }
        }
    }

    private bool CheckRequirements(Dictionary<string,int> amounts)
    {
        foreach (var i in craftableItem.ingredients)
        {
            if (!amounts.ContainsKey(i.pickup.name.ToLower()))
            {
                return false;
            }
            else if (amounts[i.pickup.name.ToLower()] < i.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateSprite()
    {
        var sr = transform.Find("SelectionBackground")
            .Find("Craftable").GetComponent<SpriteRenderer>();
        if (craftableItem.completed)
        {
            sr.sprite = craftableItem.result.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            sr.sprite = unknownSprite;
        }
    }

    public void PushOut()
    {
        if (!isOpening && !isClosing)
        {
            if (transform.Find("SelectionBackground")
                .transform.localPosition.y == 2)
            {
                return;
            }
            isOpening = true;
            StartCoroutine(Open());
        }
    }

    public void PullIn()
    {
        if (!isOpening && !isClosing)
        {
            if(transform.Find("SelectionBackground").transform.localPosition.y == 0)
            {
                return;
            }
            isClosing = true;
            StartCoroutine(Close());
        }
    }

    IEnumerator Open()
    {
        //this.gameObject.SetActive(true);
        Transform t = transform.Find("SelectionBackground").transform;
        while(t.localPosition.y < 2)
        {
            t.localPosition = new Vector3(t.localPosition.x,
                t.localPosition.y + Time.deltaTime * speed, t.localPosition.z);
            yield return null;
        }
        if (t.localPosition.y > 2)
            t.localPosition = new Vector3(t.localPosition.x,
                2, t.localPosition.z);

        isOpening = false;
        yield return null;
    }

    IEnumerator Close()
    {
        Transform t = transform.Find("SelectionBackground").transform;
        while (t.localPosition.y > 0)
        {
            t.localPosition = new Vector3(t.localPosition.x,
                t.localPosition.y - Time.deltaTime * speed, t.localPosition.z);
            yield return null;
        }
        if (t.localPosition.y < 0)
            t.localPosition = new Vector3(t.localPosition.x,
                0, t.localPosition.z);
        this.gameObject.SetActive(false);
        isClosing = false;
        yield return null;
    }
}
