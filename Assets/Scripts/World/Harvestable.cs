using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Harvestable : MonoBehaviour, IWorkable {
    
    public float speed = 20f;
    private float timeLeft, previousTimeLeft;
    public Harvest harvest;
    public SoundPlayer sp;
    bool isSoundPlay = false;
    bool contWork = true;
	// Use this for initialization
	void Start () {
        timeLeft = harvest.timeToHarvest;
	}
    private void Update()
    {
        if (timeLeft == previousTimeLeft)
        {
            isSoundPlay = false;
            sp.StopSound();
        }
        else { previousTimeLeft = timeLeft; }
    }
    public void Work()
    {
        if (contWork == false)
            return;
        if (!isSoundPlay)
        {
            isSoundPlay = true;
            sp.PlaySound(0, false, true);
        }
        previousTimeLeft = timeLeft;
        timeLeft -= Time.deltaTime * speed;
        if (timeLeft <= 0)
        {
            GenerateOutput();
        }
        else
        {
            transform.Find("Percent").transform.localScale =
                new Vector3(
                    1 - (timeLeft / harvest.timeToHarvest),
                    1);
        }
    }

    private void GenerateOutput()
    {
        contWork = false;
        StartCoroutine(WaitForSound());
    }
    

    private IEnumerator WaitForSound()
    {
        sp.PlaySound(1);
        yield return new WaitWhile(() => sp.GetComponent<AudioSource>().isPlaying);
        //GameObject.Instantiate(result, transform.position, transform.rotation, null);
        foreach (var h in harvest.possibleOut)
        {
            int r = Random.Range(1, 100);
            if (r <= h.chance)
            {
                Vector3 randomFactor = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                GameObject.Instantiate(h.pickup, transform.position + randomFactor, transform.rotation, null);
            }
        }
        Working w = GameObject.FindObjectOfType<Working>();
        if (w.closestWorkable == this)
            w.closestWorkable = null;
        w.inRangeWorkables.Remove(this);

        GameObject.Destroy(this.gameObject);
        var dm = GameObject.FindObjectOfType<DialogManager>();
        dm.queue.Enqueue(dm.dialogs[1]);
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

    public void ShowWorkDialog()
    {
        transform.Find("Harvest Button").gameObject.SetActive(true);
    }

    public void HideWorkDialog()
    {
        transform.Find("Harvest Button").gameObject.SetActive(false);
    }
}
