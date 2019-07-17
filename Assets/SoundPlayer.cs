using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour {

    [SerializeField]
    AudioClip[] clips;
    AudioSource source;
    public float timeBetweenMax = 200f, timeBetween = 0f;
    public bool isTimeBetween = false;
	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(isTimeBetween)
        {
            timeBetween--;
            if (timeBetween <= 0f)
            {
                source.Play();
                timeBetween = timeBetweenMax;
            }
        }
	}

    public void PlaySound(int index, bool loop = false, bool timeBetweenRepeat = false)
    {
        isTimeBetween = timeBetweenRepeat;
        if (isTimeBetween)
            timeBetween = timeBetweenMax;
        source.loop = loop;
        source.clip = clips[index];
        source.Play();
    }
    public void StopSound()
    {
        isTimeBetween = false;
        source.Stop();
        timeBetween = 0f;
    }
}
