using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool IsKeyPressed = false;
    public GameObject player;
    public MusicManager mm;
    public PlayerMovement pm;
    public Working w;
    public Crafting c;
    public Inventory i;
    public CameraMovement cm;
    public EnemyManager em;
    bool pauseMenu = false;
    //TODO:
    /*
     * create food prefabs
     * hunger
     */
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (
            Input.GetAxis("Submit") +
            Input.GetAxis("Cancel") +
            Input.GetAxis("ItemSelect") +
            Input.GetAxis("Work") +
            Input.GetAxis("Craft")
            == 0
            )
        {
            IsKeyPressed = false;
            pm.playerCanMove = true;
        }
        //if(Input.GetAxis("Menu") > 0)
        //{
        //    pauseMenu = true;
        //    Time.timeScale = 0;
        //}
        if (pauseMenu)
        {
            if(Input.GetAxis("Cancel") > 0)
            {
                pauseMenu = false;
                Time.timeScale = 1;
            }
        }
	}
    private void FixedUpdate()
    {
        if (pauseMenu)
        {
            if (Input.GetAxis("Cancel") > 0)
            {
                pauseMenu = false;
                Time.timeScale = 1;
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("YOUDIED");
    }

    public void Win()
    {
        var dm = GameObject.FindObjectOfType<DialogManager>();
        dm.queue.Enqueue(dm.dialogs[9]);
        //WIN THE FECKIN GAME
        pm.enabled = false;
        em.enabled = false;
        c.enabled = false;
        w.enabled = false;
        i.enabled = false;
        cm.enabled = false;
        //fade song out or stop a new song from starting
        if (mm.songIsPlaying)
        {
            mm.StartCoroutine(mm.FadeMusicOut());
        }
        else
        {
            mm.timeToStartOrEndSong = float.MaxValue;
        }
        //Debug.Log("YOUWIN");
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        var dirLight = GameObject.Find("Directional light").GetComponent<Light>();
        while(dirLight.intensity < 5.5f)
        {
            dirLight.intensity += Time.deltaTime;
            Camera.main.transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                Camera.main.transform.position.z - (Time.deltaTime * 3)
                );
            yield return null;
        }
        foreach (var lc in GameObject.FindObjectsOfType<LightCheck>())
        {
            lc.overrideCheck = true;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Win");
    }
}
