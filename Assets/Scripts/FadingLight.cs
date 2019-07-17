using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingLight : MonoBehaviour {
    
    private Light PointLight;
    private Light PointLight2;
    private CircleCollider2D lightRadius;
    bool sendDialog = false;
    public float fadeSpeed;
    public float fadeTime;
    private float currentFadeTime;
    public float maxLightRadius, maxAngle = 15, minAngle = 8;
	// Use this for initialization
	void Start () {
        lightRadius = transform.Find("Light Radius").GetComponent<CircleCollider2D>();
        PointLight = transform.Find("Point Light").GetComponent<Light>();
        PointLight2 = transform.Find("Inner Point Light").GetComponent<Light>();
        currentFadeTime = fadeTime;
        //maxSpotLightIntensity = PointLight.intensity;
    }
	
	// Update is called once per frame
	void Update () {
		if(currentFadeTime <= 0)
        {
            lightRadius.gameObject.SetActive(false);
            PointLight.gameObject.SetActive(false);
            PointLight2.gameObject.SetActive(false);
        }
        else
        {
            currentFadeTime -= fadeSpeed * Time.deltaTime;
            float fadePercent = currentFadeTime / fadeTime;
            lightRadius.radius = CalcValByPercent(fadePercent, maxLightRadius, 4);
            PointLight.range = CalcValByPercent(fadePercent, maxAngle, minAngle);
            if(fadePercent < 0.3f && !sendDialog)
            {
                var dm = GameObject.FindObjectOfType<DialogManager>();
                dm.queue.Enqueue(dm.dialogs[2]);
                sendDialog = true;
            }
        }
	}

    private float CalcValByPercent(float percent, float max, float min)
    {
        //float output = ((value - min) * 100) / (max - min);
        //float output = (percent * (max - min) / 100) + min;
        percent = percent * 100;
        float output = max - min;
        output = output * percent;
        output = output / 100;
        output = output + min;
        return output;
    }
}
