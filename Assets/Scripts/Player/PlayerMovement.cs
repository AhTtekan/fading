using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //public PlayerAnimations pa;
    public PlayerAnimations pa;
    public bool playerCanMove = true;
    public float speed;
    public SoundPlayer sp;
    bool soundPlaying = false;
	// Use this for initialization
	void Start () {
        //pa = transform.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (playerCanMove)
        {
            var x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            var y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            Vector3 move = new Vector3(x, y, transform.position.z);
            transform.Translate(move);
            if (x != 0 || y != 0)
            {
                pa.SetBool("Moving", true);
                //get direction
                if(x < 0 && x < y)
                {
                    //side, set scale to -1
                    pa.SetBool("FacingSide", true);
                    pa.SetBool("Crouching", false);
                    pa.SetBool("FacingBack", false);
                    pa.SetBool("FacingForward", false);
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if(x > 0 && x > y)
                {
                    //side, set scale to 1
                    pa.SetBool("FacingSide", true);
                    pa.SetBool("Crouching", false);
                    pa.SetBool("FacingBack", false);
                    pa.SetBool("FacingForward", false);
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if(y < 0 && y < x)
                {
                    //down, set scale to 1
                    pa.SetBool("FacingSide", false);
                    pa.SetBool("Crouching", false);
                    pa.SetBool("FacingBack", false);
                    pa.SetBool("FacingForward", true);
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                else if(y > 0 && y > x)
                {
                    //up, set scale to 1
                    pa.SetBool("FacingSide", false);
                    pa.SetBool("Crouching", false);
                    pa.SetBool("FacingBack", true);
                    pa.SetBool("FacingForward", false);
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }

                if (!soundPlaying)
                {
                    soundPlaying = true;
                    sp.PlaySound(0, false, true);
                }
            }
            else
            {
                pa.SetBool("Moving", false);
                soundPlaying = false;
                sp.StopSound();
            }
        }
        else
        {
            sp.StopSound();
            soundPlaying = false;
            pa.SetBool("Moving", false);
        }
	}
}
