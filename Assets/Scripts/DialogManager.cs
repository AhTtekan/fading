using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text text;
    public Dialog[] dialogs;
    float repeatDefaultTime = 60 * 60 * 10;//10 minutes
    public Queue<Dialog> queue;
    Dialog currentDialog;
	// Use this for initialization
	void Start () {
        foreach(var d in dialogs)
        {
            d.shown = false;
        }
        queue = new Queue<Dialog>();
        queue.Enqueue(dialogs[0]);
        StartCoroutine(Write());
    }
	
	// Update is called once per frame
	void Update () {
        
		if(queue.Count > 0 && currentDialog == null)
        {
            currentDialog = queue.Dequeue();
        }
	}

    private IEnumerator Write()
    {
        while (true)
        {
            if (currentDialog != null)
            {
                if (!currentDialog.shown)
                {
                    for (int i = 0; i < currentDialog.texts.Length; i++)
                    {
                        text.text = "";
                        foreach (var c in currentDialog.texts[i])
                        {
                            text.text += c;
                            yield return null;
                        }
                        yield return new WaitForSeconds(3f);
                        text.text = "";
                    }
                    currentDialog.shown = true;
                    currentDialog = null;
                }
                else
                {
                    currentDialog = null;
                }
            }
                yield return null;
            
        }
    }
}
