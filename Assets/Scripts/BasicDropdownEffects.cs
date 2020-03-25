using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BasicDropdownEffects : MonoBehaviour
{
    ///  Esse script é utilizado por todos os dropdowns que necessitam
    ///  mostrar todos os atributos da base. Além disso, ele precisa do
    ///  PoiManager para funcionar (onde está o script que lê e armazena
    ///  a base de dados) e o Canvas para exibir as opções no dropdown em
    ///  questão

    public string parent;
    
    private GameObject _goManager;
    private GameObject _mainCanvas;
    
    private GameObject _utils;
    
    // Start is called before the first frame update
    void Start()
    {
        _utils = GameObject.Find("Utils");
        _mainCanvas = GameObject.Find("Canvas");
        _goManager = GameObject.Find(parent);
        
        _utils.GetComponent<ProjectUtils>().DropdownOptions(GetComponent<Dropdown>(), _goManager, false);
    }
    
    // retorna a opção selecionada pelo usuário
    public void GetDropdownValue()
    {
        int index = GetComponent<Dropdown>().value + 3;
        if (index != 0) { _mainCanvas.GetComponent<CanvasBehavior>().ShowNewFilter(index); }
    }
    
    
    public void ColorChanger()
    {
        int index = GetComponent<Dropdown>().value;

        if (parent == "POIManager")
        {
            if (index != 0)
            {
                _goManager.GetComponent<PoiManagerBehavior>().UpdatePoiColorByAttribute(index + 3);
                return;
            }
            _goManager.GetComponent<PoiManagerBehavior>().UpdatePoiColorByAttribute(0);
        }
        else
        {
            if (index != 0)
            {
                _goManager.GetComponent<HeatManagerBehavior>().UpdateHeatPointColorByAttribute(index + 3);
                return;
            }
            _goManager.GetComponent<HeatManagerBehavior>().UpdateHeatPointColorByAttribute(0);
        }
    }
}
