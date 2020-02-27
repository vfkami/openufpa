using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Mapbox.Unity.Map;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PoiManagerBehavior : MonoBehaviour
{
    private GameObject[] _poiList;
    
    Dictionary<int, List<string>> filterValues = 
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

    public void RefreshVisualization()
    {
        // verifica todos os POIs da Lista
        foreach (GameObject poi in _poiList)
        {
            bool find = false;
            
            // verifica cada linha da base de dados
            foreach (string[] line in _poiInfos)
            {
                // verifica se o poi atual é o da linha consultada
                if (line[0] == poi.GetComponent<PoiClass>().myPoi.GetPoiId()) 
                {
                    //para cada filtro na lista de filtros
                    foreach (KeyValuePair<int, List<string>> filter in filterValues)
                    {
                        //para cada parâmetro dentro do atributo atual
                        foreach (string par in filter.Value)
                        {
                            if (_attributeTypes[filter.Key] != typeof(string))
                            {
                                int value = Convert.ToInt32(line[filter.Key]);
                                int minValue = Convert.ToInt32(filter.Value[0]);
                                int maxValue = Convert.ToInt32(filter.Value[1]);
                                bool invSelection = Convert.ToBoolean(filter.Value[2]);
                                
                                //seleção invertida
                                if (invSelection)
                                {
                                    print("Seleção invertida aplicada");
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
                            else
                            {
                                if (line[filter.Key] == par)
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
    public void UpdateVisualization(int index, List<string> parameters)
    {
        print("Aplicando filtros");
        try
        {
            filterValues.Add(index, parameters);
        }
        catch (ArgumentException)
        {
            filterValues[index] = parameters;
        }
        
        RefreshVisualization();
    }

    public void UpdateAltura(int index, string label)
    {
        print("Modificando altura de acordo com atributo " + label);
        int multiplier = 30;
        List<float> tList = new List<float>();

        for (int i = 0; i < _poiInfos.Count; i++)
        {
            tList.Add(float.Parse(_poiInfos[i][index], CultureInfo.InvariantCulture.NumberFormat));
        }

        List<float> normalizedList = normalizeValues(tList);
        
        for (int i = 0; i < _poiList.Length; i++)
        {
            var localScale = _poiList[i].GetComponent<Transform>().localScale;
            _poiList[i].GetComponent<Transform>().localScale = new Vector3(localScale.x, normalizedList[i] * multiplier, localScale.z);
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
