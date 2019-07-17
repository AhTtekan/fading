using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

    public AudioSource aus;
    public AudioClip[] songs;
    public bool fading = false,
        songIsPlaying = false;
    public float timeSinceLastSong;
    public float timeToStartOrEndSong;
	// Use this for initialization
	void Start () {
        timeToStartOrEndSong = 30;// Random.Range(0, 60 * 30);
	}

    // Update is called once per frame
    void Update()
    {
        if (!fading)
        {
            timeToStartOrEndSong--;
            if (timeToStartOrEndSong <= 0)
            {
                timeToStartOrEndSong = Random.Range(60 * 60, 60 * 60 * 4);
                songIsPlaying = !songIsPlaying;
                if (songIsPlaying)
                {
                    //choose song
                    int index = Random.Range(0, songs.Length);
                    Debug.Log(index);
                    aus.clip = songs[index];
                    aus.volume = 0;
                    StartCoroutine(FadeMusicIn());
                }
                else
                {
                    timeToStartOrEndSong /= 2;
                    StartCoroutine(FadeMusicOut());
                }
            }
        }
    }

    public IEnumerator FadeMusicIn()
    {
        aus.Play();
        fading = true;
        while (aus.volume < 0.4f)
        {
            aus.volume += Time.deltaTime * 0.2f;
            yield return null;
        }
        fading = false;
    }
    public IEnumerator FadeMusicOut()
    {
        fading = true;
        while (aus.volume > 0f)
        {
            var val = aus.volume - Time.deltaTime * 0.2f;
            aus.volume = val < 0 ? 0 : val;
            yield return null;
        }
        aus.Stop();
        fading = false;
    }
}
