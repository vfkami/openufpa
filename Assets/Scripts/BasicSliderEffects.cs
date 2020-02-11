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
    int _id;
    string _label;
    

    public void OnValueChanged()
    {
        GameObject poiManager = GameObject.Find("POIManager");
        poiManager.GetComponent<PoiManagerBehavior>().UpdateVisualizationContinuum(_id, 
            new Vector2(GetComponentInChildren<MinMaxSlider>().Values.minValue, 
                GetComponentInChildren<MinMaxSlider>().Values.maxValue));
    }

    public void setSliderBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }

    public int getID() {return _id;}
    
    
    
}
