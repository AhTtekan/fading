using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSelector: MonoBehaviour {

    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        //choose sprite
        sr.sprite = sprites[Random.Range(0, sprites.Length - 1)];
	}
}