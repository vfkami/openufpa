﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour
{
    //Out of Canvas GOs
    public GameObject poiManager;

    //Prefabs
    private GameObject _checkGroupTemplate;
    private GameObject _minMaxSliderTemplate;
    
    //Canvas GOs
    public GameObject filterMenu;
    public List<GameObject> listOfSliders;
    public List<GameObject> listOfCheckGroup;

    public GameObject dpd_Altura;
    public GameObject dpd_Largura;

    //Variables
    private bool _filterVisibility;
    private List<string[]> _poiInfos;
    private List<Type> _newTypeList;
    private List<string> _newOptions;

    private List<String> Options;
    private List<Type> Tipos;
    
    private void Awake()
    {
        _checkGroupTemplate = GameObject.Find("Template_CheckboxGroup");
        _minMaxSliderTemplate = GameObject.Find("Template_MinMaxSlider");
        _minMaxSliderTemplate.SetActive(false);
        _checkGroupTemplate.SetActive(false);
        filterMenu.SetActive(false);
    }

    void Start()
    {
        Tipos = poiManager.GetComponent<DatasetReader>().GetLabelTypes();
        _poiInfos = poiManager.GetComponent<DatasetReader>().GetPoiList();

        populateHWDropdown(poiManager.GetComponent<DatasetReader>().GetDatabaseLabel(), Tipos, dpd_Altura);
        populateHWDropdown(poiManager.GetComponent<DatasetReader>().GetDatabaseLabel(), Tipos, dpd_Largura);
    }

    void Update()
    {
        Options = poiManager.GetComponent<DatasetReader>().GetDatabaseLabel();
        Tipos = poiManager.GetComponent<DatasetReader>().GetLabelTypes();
    }

    public void ChangeFilterMenuVisibility()
    {
        _filterVisibility = !_filterVisibility;
        filterMenu.SetActive(_filterVisibility);
    }

    public void DropdownOptionReturn(int index, string label, Type tipo)
    {
        bool find = false;
        if (tipo == typeof(Int32) || tipo == typeof(float))
        {
            foreach (GameObject check in listOfCheckGroup)
                check.SetActive(false);
            
            if (listOfSliders.Count != 0)
            {
                foreach (var slider in listOfSliders)
                {
                    if (slider.GetComponent<BasicSliderEffects>().getID() != index)
                        slider.SetActive(false);
                    else
                    {
                        slider.SetActive(true);
                        find = true;
                    }
                }
            }
            if (!find)
            {
                GameObject newSlider = InstantiateNewSlider(index, label);

                if (tipo == typeof(int))
                    newSlider.GetComponentInChildren<MinMaxSlider>().wholeNumbers = true;
                
                newSlider.GetComponentInChildren<MinMaxSlider>().UpdateText();
                listOfSliders.Add(newSlider);
            }
        }
        else
        {
            foreach (GameObject slider in listOfSliders)
                slider.SetActive(false);

            if (listOfCheckGroup.Count != 0)
            {
                foreach (var checkGroup in listOfCheckGroup) //verificar se já existe
                {
                    checkGroup.SetActive(false);
                    if (checkGroup.GetComponent<CheckGroupEffects>().getID() == index) //se ja existe: ativa
                    {
                        checkGroup.SetActive(true);
                        find = true;
                    }
                }
            }

            if (!find)//se nao existe = instancia novo
            {
                GameObject newCheckGroup = InstantiateNewCheckGroup(index, label);
                listOfCheckGroup.Add(newCheckGroup);
            }
        }
    }

    private GameObject InstantiateNewCheckGroup(int index, string label)
    {
        List<string> categories = GetAllCategoriesFromAttribute(index);
        GameObject checkgroup = Instantiate(_checkGroupTemplate, filterMenu.transform);
        
        checkgroup.GetComponent<RectTransform>().position = _checkGroupTemplate.GetComponent<RectTransform>().position;
        checkgroup.name = "CheckGroup_" + listOfCheckGroup.Count;
        checkgroup.GetComponent<CheckGroupEffects>().setGroupBasics(index, label);
        checkgroup.GetComponent<CheckGroupEffects>().UpdateCheckBoxes(categories);
        checkgroup.SetActive(true);

        return checkgroup;
    }

    private GameObject InstantiateNewSlider(int index, string label)
    {
        Vector2 minMax = GetMinMaxValueFromAttribute(index);
        GameObject slider = Instantiate(_minMaxSliderTemplate, filterMenu.transform);

        slider.GetComponent<RectTransform>().localPosition = _minMaxSliderTemplate.GetComponent<RectTransform>().localPosition;
        slider.name = "Slider_" + listOfSliders.Count;
        slider.GetComponent<BasicSliderEffects>().setSliderBasics(index, label);
        slider.GetComponentInChildren<MinMaxSlider>().SetLimits(minMax.x, minMax.y);
        slider.SetActive(true);

        return slider;
    }
    

    private Vector2 GetMinMaxValueFromAttribute(int index)
    {
        Vector2 minMaxValue; // MinMaxValue[0] = Min, MinMaxValue[1] = Max
        
        int min = 99999, 
            max = 0;
        
        foreach (string[] line in _poiInfos)
        {
            int.TryParse(line[index], out var value);
            if (min >= value)
                min = value;
            
            if (max <= value)
                max = value;
        }

        minMaxValue.x = min;
        minMaxValue.y = max;
        return minMaxValue;
    }

    private List<string> GetAllCategoriesFromAttribute(int index)
    {
        List<string> categories = new List<string>();
        foreach (string[] line in _poiInfos)
        {
            string tempCategory = line[index];
            if (!categories.Contains(tempCategory))
            {
                categories.Add(tempCategory);
            }
        }
        return categories;
    }

    void populateHWDropdown(List<string> options, List<Type> optionsTypes, GameObject dropdown)
    {
        List<string> cloneOptions = new List<string>(options);
        List<Type> cloneTypes = new List<Type>(optionsTypes);
        
        dropdown.GetComponent<Dropdown>().ClearOptions();
        
        // Remove Exceptions
        for (int i = 0; i < 4; i++)
        {
            cloneOptions.RemoveAt(0);
            cloneTypes.RemoveAt(0);
        }

        // Add Default Button
        List<string> tempOptions = new List<string> {"- selecione -"};

        for (int i = 0; i < cloneTypes.Count; i++)
        {
            if (cloneTypes[i] != typeof(String) && cloneTypes[i] != typeof(bool))
            {
                tempOptions.Add(cloneOptions[i]);
            }
        }
        
        dropdown.GetComponent<Dropdown>().AddOptions(tempOptions);
}
}

