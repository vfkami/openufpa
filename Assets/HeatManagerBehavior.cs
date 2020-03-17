using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class HeatManagerBehavior : MonoBehaviour
{
    private GameObject[] _heatList;

    private List<string[]> _heatInfos;
    private List<string> _heatLabels;
    private List<Type> _attributeHTypes;

    private void Start()
    {
        
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
        int multiplier = 30;

        List<float> normalizedList = normalizeValues(index);
        
        for (int i = 0; i < _heatList.Length; i++)
        {
            var localScale = _heatList[i].GetComponent<Transform>().localScale;
            _heatList[i].GetComponent<SinglePOIBehavior>().SizeSetter(new Vector3(localScale.x, (normalizedList[i] + + 0.5F) * multiplier, localScale.z));
        }    
    }
    
    List<float> normalizeValues(int index) // criar utils script
    {
        List<float> tList = new List<float>();
        foreach (string[] info in _heatInfos)
        {
            tList.Add(float.Parse(info[index], CultureInfo.InvariantCulture.NumberFormat));
        }
        
        float minValue = tList.Min();
        float maxValue = tList.Max();
        
        List<float> normalizedValues = new List<float>();
        foreach (var value in tList)
        {
            float tempValue = (value - minValue)/(maxValue - minValue);
            normalizedValues.Add(tempValue);
        }
        return normalizedValues;
    }

}
