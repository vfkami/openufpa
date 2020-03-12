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
    // Esse script dita as regras de cada Slider para funcionar como
    // regulador do filtro contínuo para cada atributo na base
    
    private int _id;
    private string _label;
    
    // chamado em tempo de execução, no Editor, quando o valor do Slider muda.
    // Essa função passa como parâmetro os valores definidos no slider para a
    // função responsável por armazenar a lista de filtros aplicados
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

    // Armazena algumas informações fundamentais
    // para o slider quando ele for instanciado
    public void SetSliderBasics(int index, string attributeName)
    {
        _id = index;
        _label = attributeName;
    }

    public int GetId() {return _id;}
    
    
    
}
