using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderControl : MonoBehaviour
{
    public Planet planet;             // 需要控制的行星
    public Slider strengthSlider;     // 控制 strength
    public Slider centreXSlider;      // 控制 centre.x
    public Slider numLayersSlider;    // 控制 numLayers
    public TMP_Text strengthValueText;    // 显示 strength 数值
    public TMP_Text centreXValueText;     // 显示 centre.x 数值
    public TMP_Text numLayersValueText;   // 显示 numLayers 数值
    public Button combineNoise;
    public Button setMask;
    public int noiseLayerIndex = 1;


    private NoiseSettings noiseSettings;

    void Start()
    {
        if (planet != null)
        {
            noiseSettings = planet.shapeSettings.noiseLayers[0].noiseSettings;
            
            // 初始化滑块值
            if (strengthSlider != null)
            {
                strengthSlider.value = noiseSettings.perlinNoiseSettings.strength;
                strengthSlider.onValueChanged.AddListener(UpdateStrength);
                UpdateStrength(noiseSettings.perlinNoiseSettings.strength);
            }

            if (centreXSlider != null)
            {
                centreXSlider.value = noiseSettings.perlinNoiseSettings.centre.x;
                centreXSlider.onValueChanged.AddListener(UpdateCentreX);
                UpdateCentreX(noiseSettings.perlinNoiseSettings.centre.x);
            }

            if (numLayersSlider != null)
            {
                numLayersSlider.value = noiseSettings.perlinNoiseSettings.numLayers;
                numLayersSlider.onValueChanged.AddListener(UpdateNumLayers);
                UpdateNumLayers(noiseSettings.perlinNoiseSettings.numLayers);
            }

                if (combineNoise != null)
            {
                combineNoise.onClick.AddListener(ToggleEnabled);
            }

            if (setMask != null)
            {
                setMask.onClick.AddListener(ToggleUseMask);
            }
        }
    }

    void UpdateStrength(float value)
    {
        if (noiseSettings != null)
        {
            noiseSettings.perlinNoiseSettings.strength = value;
            if (strengthValueText != null) strengthValueText.text = value.ToString("F1");
            planet.GeneratePlanet(); // 重新生成行星
        }
    }

    void UpdateCentreX(float value)
    {
        if (noiseSettings != null)
        {
            noiseSettings.perlinNoiseSettings.centre = new Vector3(value, noiseSettings.perlinNoiseSettings.centre.y, noiseSettings.perlinNoiseSettings.centre.z);
            if (centreXValueText != null) centreXValueText.text = value.ToString("F1");
            planet.GeneratePlanet(); // 重新生成行星
        }
    }

    void UpdateNumLayers(float value)
    {
        if (noiseSettings != null)
        {
            noiseSettings.perlinNoiseSettings.numLayers = Mathf.RoundToInt(value); // 确保是整数
            if (numLayersValueText != null) numLayersValueText.text = noiseSettings.perlinNoiseSettings.numLayers.ToString("F0");
            planet.GeneratePlanet(); // 重新生成行星
        }
    }

    void ToggleEnabled()
    {
        if (planet != null && planet.shapeSettings.noiseLayers.Length > noiseLayerIndex)
        {
            planet.shapeSettings.noiseLayers[noiseLayerIndex].enabled = 
                !planet.shapeSettings.noiseLayers[noiseLayerIndex].enabled;
            
            planet.GeneratePlanet();  // 重新生成行星
        }
    }

    void ToggleUseMask()
    {
        if (planet != null && planet.shapeSettings.noiseLayers.Length > noiseLayerIndex)
        {
            planet.shapeSettings.noiseLayers[noiseLayerIndex].useFirstLayerAsMask = 
                !planet.shapeSettings.noiseLayers[noiseLayerIndex].useFirstLayerAsMask;
            
            planet.GeneratePlanet();  // 重新生成行星
        }
    }
}
