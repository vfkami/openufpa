using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PoiManagerBehavior : MonoBehaviour
{
    private GameObject[] _poiList;
    
    Dictionary<int, List<string>> filterValues = //implementar esta bagaça amanhã
        new Dictionary<int, List<string>>();
    
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
                            if (_attributeTypes[attribute.Key - 3] != typeof(String))
                            {
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


}
