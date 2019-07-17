using UnityEngine;
using UnityEngine.SceneManagement;

public class youdied : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Submit") > 0)
        {
            SceneManager.LoadScene("Layout");
        }
        else if(Input.GetAxis("Menu") > 0)
        {
            Application.Quit();
        }
	}
}
