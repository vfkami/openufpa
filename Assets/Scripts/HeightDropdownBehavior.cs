using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDropdownBehavior : MonoBehaviour
{
    /// Esse script é utilizado por Dropdowns de Altura e é responsável por
    /// passar o atributo selecionado que irá determinar a altura dos POI's

    private GameObject goManager;
    public string parent;

    private void Start()
    {
        goManager = GameObject.Find(parent);
        PopulateDropdown();
    }

    public void OnValueChanged()
    {
        int index = GetComponent<Dropdown>().value;
        string label = GetComponent<Dropdown>().options[index].text;

        List<string> databaseLabels = new List<string>(goManager.GetComponent<DatasetReader>().GetDatabaseLabel());

        for (int i = 0; i < databaseLabels.Count; i++)
        {
            if (databaseLabels[i] == label)
            {
                index = i;
                break;
            }
        }
        if (parent == "POIManager") 
           goManager.GetComponent<PoiManagerBehavior>().UpdateHeight(index, label);
        else
            goManager.GetComponent<HeatManagerBehavior>().UpdateWeight(index, label);

    }
    
    void PopulateDropdown()
    {
        List<string> options = new List<string>(goManager.GetComponent<DatasetReader>().GetDatabaseLabel());
        List<Type> types = new List<Type>(goManager.GetComponent<DatasetReader>().GetLabelTypes());
        
        GetComponent<Dropdown>().ClearOptions();
        
        // Remove Exceptions
        for (int i = 0; i < 4; i++)
        {
            options.RemoveAt(0);
            types.RemoveAt(0);
        }

        // Add Default Button
        List<string> tempOptions = new List<string> {"- selecione -"};

        for (int i = 0; i < types.Count; i++)
        {
            if (types[i] != typeof(String) && types[i] != typeof(bool))
            {
                tempOptions.Add(options[i]);
            }
        }
        
        GetComponent<Dropdown>().AddOptions(tempOptions);
    }
}
