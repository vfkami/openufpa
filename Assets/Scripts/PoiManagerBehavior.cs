using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Mapbox.Unity.Map;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PoiManagerBehavior : MonoBehaviour
{
    private GameObject[] _poiList;
    
    Dictionary<int, List<string>> filterValues = //implementar esta bagaça amanhã
        new Dictionary<int, List<string>>();
    
    private List<bool> invertedSelection = new List<bool>();
    private List<string[]> _poiInfos;
    private List<string> _poiLabels;
    private List<Type> _attributeTypes;

    private void Start()
    {
        _poiInfos = GetComponent<DatasetReader>().GetPoiList();
        _poiLabels = GetComponent<DatasetReader>().GetDatabaseLabel();
        _attributeTypes = GetComponent<DatasetReader>().GetLabelTypes();
    }
    void Update()
    {
        _poiList = GameObject.FindGameObjectsWithTag("poi");
    }

    public void UpdateVisualization(int index, List<string> parameters, bool invertedSelection)
    {
        try
        {
            filterValues.Add(index, parameters);
        }
        catch (ArgumentException)
        {
            filterValues[index] = parameters;
        }
        
        foreach (var poi in _poiList) //Verifica em cada POI da Lista
        {
            bool find = false;
            foreach (var line in _poiInfos) //Verifica em cada linha da Base de dados
            {
                if (line[0] == poi.GetComponent<PoiClass>().myPoi.GetPoiId()) //verifica se o POI consultado é o mesmo achado na linha consultada
                {
                    foreach (var attribute in filterValues) // para cada filtro aplicado na lista de filtros
                    {
                        foreach (string parameter in attribute.Value)
                        {
                            print(_attributeTypes[attribute.Key]);
                            
                            print(parameter);
                            
                            if (_attributeTypes[attribute.Key] != typeof(String))
                            {
                                print("tipo string");
                                int value = Convert.ToInt32(line[attribute.Key]);
                                int minValue = Convert.ToInt32(attribute.Value[0]);
                                int maxValue = Convert.ToInt32(attribute.Value[1]);
                                
                                if (invertedSelection && index == attribute.Key)
                                {
                                    if (!(value < minValue || value > maxValue))
                                    {
                                        poi.GetComponent<Renderer>().enabled = false;
                                        find = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!(value >= minValue && value <= maxValue))
                                    {
                                        poi.GetComponent<Renderer>().enabled = false;
                                        find = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if(!find)
                    poi.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public void UpdateVisualization(int index, List<string> parameters)
    {
        try
        {
            filterValues.Add(index, parameters);
        }
        catch (ArgumentException)
        {
            filterValues[index] = parameters;
        }

        foreach (var values in filterValues.Values)
        {
            foreach (var el in values)
            {
                print(el);
            }
        }
        
        foreach (var poi in _poiList) //Verifica em cada POI da Lista
        {
            bool find = false;
            foreach (var line in _poiInfos) //Verifica em cada linha da Base de dados
            {
                if (line[0] == poi.GetComponent<PoiClass>().myPoi.GetPoiId()) //verifica se o POI consultado é o mesmo achado na linha consultada
                {
                    foreach (var attribute in filterValues) // para cada filtro aplicado na lista de filtros
                    {
                        foreach (string parameter in attribute.Value)
                        {
                            print(_attributeTypes[attribute.Key]);
                            print(parameter);
                            
                            print("tipo string de verdade");
                            if (line[attribute.Key] == parameter)
                            {
                                poi.GetComponent<Renderer>().enabled = false;
                                find = true;
                                break;
                            }
                        }
                    }
                }
                if(!find)
                    poi.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    public void updateAltura(int index, string label)
    {
        int multiplier = 10;
        List<float> tList = new List<float>();
        print(index);

        for (int i = 0; i < _poiInfos.Count; i++)
        {
            tList.Add(float.Parse(_poiInfos[i][index], CultureInfo.InvariantCulture.NumberFormat));
        }

        List<float> normalizedList = normalizeValues(tList);

        foreach (var value in normalizedList)
        {
            print(value);
        }
        
        for (int i = 0; i < _poiList.Length; i++)
        {
            var localScale = _poiList[i].GetComponent<Transform>().localScale;
            _poiList[i].GetComponent<Transform>().localScale = new Vector3(localScale.x, normalizedList[i] * multiplier, localScale.z);
        }
        
    }

    public void updateLargura(int index, string label)
    {
        int multiplier = 3;
        List<float> tList = new List<float>();
        print(index);

        for (int i = 0; i < _poiInfos.Count; i++)
        {
            tList.Add(float.Parse(_poiInfos[i][index], CultureInfo.InvariantCulture.NumberFormat));
        }

        List<float> normalizedList = normalizeValues(tList);

        foreach (var value in normalizedList)
        {
            print(value);
        }
        
        for (int i = 0; i < _poiList.Length; i++)
        {
            var localScale = _poiList[i].GetComponent<Transform>().localScale;
            _poiList[i].GetComponent<Transform>().localScale = new Vector3(normalizedList[i] * multiplier, localScale.y, localScale.z);
        }    
    }

    List<float> normalizeValues(List<float> values)
    {
        float minValue = values.Min();
        float maxValue = values.Max();
        
        List<float> normalizedValues = new List<float>();
        foreach (var value in values)
        {
            float tempValue = (value - minValue + - 1)/(maxValue + 1 - minValue - 1) + 0.5F;
            normalizedValues.Add(tempValue);
        }
        return normalizedValues;
    }
    


}
