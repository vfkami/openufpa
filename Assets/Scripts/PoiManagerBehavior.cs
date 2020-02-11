using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PoiManagerBehavior : MonoBehaviour
{
    private GameObject[] _poiList;
    
    
    private List<string[]> _poiInfos;
    private List<string> _poiLabels;

    private void Start()
    {
        _poiInfos = GetComponent<DatasetReader>().GetPoiList();
        _poiLabels = GetComponent<DatasetReader>().GetDatabaseLabel();
    }
    void Update()
    {
        _poiList = GameObject.FindGameObjectsWithTag("poi");
    }

    public void UpdateVisualizationContinuum(int index, Vector2 values)
    {
        foreach (var poi in _poiList)
        {
            string poiId = poi.GetComponent<PoiClass>().myPoi.GetPoiId();
            foreach (var line in _poiInfos)
            {
                string lineId = line[0];
                if (lineId == poiId)
                {
                    int value = Convert.ToInt32(line[index]);
                    print(values.x + "" + values.y); 
                    if (value < values.x || value > values.y)
                        poi.GetComponent<Renderer>().enabled = false;
                    else
                        poi.GetComponent<Renderer>().enabled = true;
                    
                    break;
                }
            }
        }
    }
    
    public static bool Between(int num, float lower, float upper, bool inclusive = false)
    {
        return inclusive
            ? lower <= num && num <= upper
            : lower < num && num < upper;
    }
    
    
}
