using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Esse script coordena todos os elementos do Canvas.
 */

public class CanvasBehavior : MonoBehaviour
{
    //Out of Canvas GOs
    public GameObject poiManager;

    //Prefabs
    private GameObject _checkGroupTemplate;
    private GameObject _minMaxSliderTemplate;
    
    //Canvas GOs
    public GameObject filterMenu;
    public GameObject heatMenu;
    public List<GameObject> listOfSliders;
    public List<GameObject> listOfCheckGroup;
    public GameObject poi_dpdAltura;
    public GameObject ht_dpdAltura;
    private GameObject _poiInfoDisplay;
    private GameObject _canvasHeader;

    private GameObject _utils;
    
    //Variables
    private bool _filterVisibility;
    private List<string[]> _poiInfos;
    private List<Type> _newTypeList;
    private List<string> _newOptions;

    private List<String> _options;
    private List<Type> _tipos;
    
    private void Awake()
    {
        _poiInfoDisplay = GameObject.Find("POInfo");
        _checkGroupTemplate = GameObject.Find("Template_CheckboxGroup");
        _minMaxSliderTemplate = GameObject.Find("Template_MinMaxSlider");
        _canvasHeader = GameObject.Find("CanvasHeader");
        _utils = GameObject.Find("Utils");
        
        _minMaxSliderTemplate.SetActive(false);
        _checkGroupTemplate.SetActive(false);
        filterMenu.SetActive(false);
        heatMenu.SetActive(false);
        _poiInfoDisplay.SetActive(false);
        _poiInfoDisplay.gameObject.SetActive(false);
    }

    private void Start()
    {
        _tipos = poiManager.GetComponent<DatasetReader>().GetLabelTypes();
        _poiInfos = poiManager.GetComponent<DatasetReader>().GetPoiList();

        //populateHWDropdown(poiManager.GetComponent<DatasetReader>().GetDatabaseLabel(), _tipos, poi_dpdAltura);

    }
    
    // Quando o botão de menu for pressionado
    public void ChangeFilterMenuVisibility()
    {
        _filterVisibility = !_filterVisibility;
        filterMenu.SetActive(_filterVisibility);
        heatMenu.SetActive(_filterVisibility);
    }

    public void GetDropdownOption(int index, string label, Type tipo)
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
                    if (slider.GetComponent<BasicSliderEffects>().GetId() != index)
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
                    if (checkGroup.GetComponent<CheckGroupEffects>().GetId() == index) //se ja existe: ativa
                    {
                        checkGroup.SetActive(true);
                        find = true;
                    }
                }
            }

            if (find) return;
            GameObject newCheckGroup = InstantiateNewCheckGroup(index, label);
            listOfCheckGroup.Add(newCheckGroup);
        }
    }

    private GameObject InstantiateNewCheckGroup(int index, string label)
    {
        List<string> categories = _utils.GetComponent<ProjectUtils>().GetAllCategoriesFromAttribute(index, _poiInfos);
        GameObject checkgroup = Instantiate(_checkGroupTemplate, filterMenu.transform);
        
        checkgroup.GetComponent<RectTransform>().position = _checkGroupTemplate.GetComponent<RectTransform>().position;
        checkgroup.name = "CheckGroup_" + listOfCheckGroup.Count;
        checkgroup.GetComponent<CheckGroupEffects>().SetGroupBasics(index, label);
        checkgroup.GetComponent<CheckGroupEffects>().UpdateCheckBoxes(categories);
        checkgroup.SetActive(true);

        return checkgroup;
    }

    private GameObject InstantiateNewSlider(int index, string label)
    {
        Vector2 minMax = _utils.GetComponent<ProjectUtils>().GetMinMaxValueFromAttribute(index, _poiInfos);
        GameObject slider = Instantiate(_minMaxSliderTemplate, filterMenu.transform);

        slider.GetComponent<RectTransform>().localPosition = _minMaxSliderTemplate.GetComponent<RectTransform>().localPosition;
        slider.name = "Slider_" + listOfSliders.Count;
        slider.GetComponent<BasicSliderEffects>().SetSliderBasics(index, label);
        slider.GetComponentInChildren<MinMaxSlider>().SetLimits(minMax.x, minMax.y);
        slider.SetActive(true);

        return slider;
    }
    
    public void UpdatePoiDisplayed(string poiName)
    {
         _poiInfoDisplay.gameObject.SetActive(true);
         _poiInfoDisplay.GetComponentInChildren<Text>().text = "";

         List<string> labels = poiManager.GetComponent<DatasetReader>().GetDatabaseLabel();
         
         string[] poiInfos = poiManager.GetComponent<DatasetReader>().getPoiInformation(poiName);

         for (int i = 0; i < poiInfos.Length; i++)
         {
             _poiInfoDisplay.GetComponentInChildren<Text>().text += labels[i] + ": " + poiInfos[i];

             if (poiInfos.Length - i > 1)
             {
                 _poiInfoDisplay.GetComponentInChildren<Text>().text += ",\n";
             }
         }
    }

    public void HidePOIInfo()
    {
        _poiInfoDisplay.gameObject.SetActive(false);
    }

    public void UpdateHeaderText(string poiName)
    {
        foreach (var line in _poiInfos)
        {
            if (line[0] == poiName)
            {
                _canvasHeader.GetComponentInChildren<Text>().text = line[3];
                return;
            }
        }
        _canvasHeader.GetComponent<Text>().text = poiName;
    }
    
}

