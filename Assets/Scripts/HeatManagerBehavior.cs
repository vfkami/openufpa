using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class HeatManagerBehavior : MonoBehaviour
{
    private GameObject[] _heatList;
    private GameObject _utils;

    private List<string[]> _heatInfos;
    private List<string> _heatLabels;
    private List<Type> _attributeHTypes;
    
    private void Start()
    {
        _utils = GameObject.Find("Utils");
        
        _heatInfos = GetComponent<DatasetReader>().GetPoiList();
        _heatLabels = GetComponent<DatasetReader>().GetDatabaseLabel();
        _attributeHTypes = GetComponent<DatasetReader>().GetLabelTypes();
    }

    private void Update()
    {
        _heatList = GameObject.FindGameObjectsWithTag("heatpoint");
    }


    // Start is called before the first frame update
    public void UpdateWeight(int index, string label)
    {
        
        print("Modificando altura de acordo com atributo " + label);
        int multiplier = 10;

        List<float> normalizedList = _utils.GetComponent<ProjectUtils>().NormalizeValues(index, _heatInfos);
        
        for (int i = 0; i < _heatList.Length; i++)
        {
            Vector3 localScale = _heatList[i].GetComponent<Transform>().localScale;
            float newValue = (float)((normalizedList[i] + 0.5) * multiplier);
            _heatList[i].GetComponent<SinglePOIBehavior>().SizeSetter(new Vector3(newValue, localScale.y, newValue));
        }    
    }
    
    public void UpdateHeatPointColorByAttribute(int attIndex)
    {
        if (attIndex == 0) //se nenhum atributo for escolhido
        {
            foreach (var poi in _heatList)
            {
                poi.GetComponent<SinglePOIBehavior>().ColorSetter(Color.gray);
            }

            return;
        }

        List<float> normalizedList = _utils.GetComponent<ProjectUtils>().NormalizeValues(attIndex, _heatInfos);
        for (int i = 0; i < _heatList.Length; i++)
        {
            Color newColor = Color.HSVToRGB(1, normalizedList[i] + 0.1F, 1);
            newColor.a = 0.5F;
            _heatList[i].GetComponent<SinglePOIBehavior>().ColorSetter(newColor);
        }
    }

    public void UpdateVisibility()
    {
        for (int i = 0; i < _heatList.Length; i++)
        {
            bool heatvisibility = _heatList[i].GetComponent<Renderer>().enabled;
            _heatList[i].GetComponent<Renderer>().enabled = !heatvisibility;
        }

    }

}
