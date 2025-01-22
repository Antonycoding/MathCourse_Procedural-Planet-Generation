using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NoiseSettings {

    public enum FilterType { Simple, Ridgid };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public PerlinNoiseSettings perlinNoiseSettings;
    
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class PerlinNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;
        public Vector3 centre;
        public float minValue;

        
    }

    [System.Serializable]
    public class RidgidNoiseSettings : PerlinNoiseSettings
    {
        public float weightMultiplier = .8f;
    }



}
