using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance;
    public GameObject globalLight;
    public GameObject windowLight;
    public GameObject windowReflection;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        UpdateLights();
    }

    public void UpdateLights()
    {
        if(GameManager.instance.currentTimeOfDay == GameManager.TimeOfDay.Night)
        {
            globalLight.GetComponent<Light2D>().intensity = 0.18f;
            windowLight.GetComponent<Light2D>().color = new Color(0 / 255f, 117 / 255f, 255 / 255f, 1.0f);
            windowReflection.GetComponent<Light2D>().color = new Color(69 / 255f, 217 / 255f, 255 / 255f, 1.0f);
        }
        else
        {
            globalLight.GetComponent<Light2D>().intensity = 1.0f;
            windowLight.GetComponent<Light2D>().color = new Color(245 / 255f, 255 / 255f, 0 / 255f, 1.0f);
            windowReflection.GetComponent<Light2D>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 1.0f);
        }
    }
}
