using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Change());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Change()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Layout");
    }
}
