using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mapbox.Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicSliderEffects : MonoBehaviour
{
    private int _id;
    private string _label;
    
    public void OnValueChanged()
    {
        GameObject poiManager = GameObject.Find("POIManager");

        List<String> parameters = new List<string> {
            GetComponentInChildren<MinMaxSlider>().Values.minValue.ToString(),
            GetComponentInChildren<MinMaxSlider>().Values.maxValue.ToString(),
            GetComponentInChildren<Toggle>().isOn.ToString()
        };
        
        poiManager.GetComponent<PoiManagerBehavior>().UpdateFilterList(_id, parameters);
    }

    public void SetSliderBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }

    public int GetId() {return _id;}
    
    
    
}
