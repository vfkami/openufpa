using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDropdownBehavior : MonoBehaviour
{
    /// Esse script é utilizado pelo GO Dpd_Altura e é responsável por
    /// passar o atributo selecionado que irá determinar a altura dos POI's

    private GameObject _poiManager;
    
    public void OnValueChanged()
    {
        _poiManager = GameObject.Find("POIManager");
        int index = GetComponent<Dropdown>().value;
        string label = GetComponent<Dropdown>().options[index].text;

        List<string> databaseLabels = new List<string>(_poiManager.GetComponent<DatasetReader>().GetDatabaseLabel());

        for (int i = 0; i < databaseLabels.Count; i++)
        {
            if (databaseLabels[i] == label)
            {
                index = i;
                break;
            }
        }
        _poiManager.GetComponent<PoiManagerBehavior>().UpdateAltura(index, label);
    }


}
