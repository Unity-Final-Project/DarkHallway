using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Light areaLight; // Reference to the area light
    public Renderer lampRenderer; // Reference to the renderer of the lamp
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 1.5f; // Maximum light intensity
    public float flickerSpeed = 0.1f; // Speed of the flickering effect
    public Color emissionColor = Color.white; // Emission color of the lamp

    private float targetIntensity;
    private float changeSpeed;

    void Start()
    {
        if (areaLight == null)
        {
            areaLight = GetComponent<Light>();
        }

        if (lampRenderer == null)
        {
            Debug.LogError("Lamp Renderer is not assigned!");
            return;
        }

        targetIntensity = areaLight.intensity;
        changeSpeed = flickerSpeed;
    }

    void Update()
    {
        if (Mathf.Approximately(areaLight.intensity, targetIntensity))
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            changeSpeed = Random.Range(flickerSpeed * 0.2f, flickerSpeed * 0.1f);
        }

        areaLight.intensity = Mathf.MoveTowards(areaLight.intensity, targetIntensity, changeSpeed * Time.deltaTime);
        UpdateLampEmission(areaLight.intensity);
    }

    void UpdateLampEmission(float intensity)
    {
        if (lampRenderer != null)
        {
            Material lampMaterial = lampRenderer.material;
            Color finalColor = emissionColor * Mathf.LinearToGammaSpace(intensity);
            lampMaterial.SetColor("_EmissionColor", finalColor);
            DynamicGI.SetEmissive(lampRenderer, finalColor);
        }
    }
}
