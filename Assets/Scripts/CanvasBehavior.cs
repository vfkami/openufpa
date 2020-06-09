using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/*
 * Esse script coordena todos os elementos do Canvas.
 */

public class CanvasBehavior : MonoBehaviour
{
    //Out of Canvas GOs
    private GameObject _poiManager;
    private GameObject _heatManager;

    //Prefabs
    public GameObject checkGroupTemplate;
    public GameObject minMaxSliderTemplate;
    
    //Canvas GOs
    public GameObject filterMenu;
    public GameObject heatMenu;


    private List<GameObject> _filters = new List<GameObject>();
    
    private GameObject _poiInfoDisplay;
    private GameObject _canvasHeader;

    private GameObject _utils;
    
    //Variables
    private bool _filterVisibility;
    
    private List<string> _poiLabels;
    private List<string[]> _poiInfos;
    private List<Type> _poiLabelTypes;

    

    private void Awake()
    {
        _poiInfoDisplay = GameObject.Find("POInfo");
        _canvasHeader = GameObject.Find("CanvasHeader");
        _utils = GameObject.Find("Utils");
        
        filterMenu.SetActive(false);
        heatMenu.SetActive(false);
        _poiInfoDisplay.SetActive(false);
        _poiInfoDisplay.gameObject.SetActive(false);
    }

    private void Start()
    {
        _poiManager = GameObject.Find("POIManager");
        _heatManager = GameObject.Find("HeatManager");
        
        
        _poiInfos = _poiManager.GetComponent<DatasetReader>().GetPoiList();
        _poiLabels = _poiManager.GetComponent<DatasetReader>().GetDatabaseLabel();
        _poiLabelTypes = _poiManager.GetComponent<DatasetReader>().GetLabelTypes();
    }
    
    // Quando o botão de menu for pressionado
    public void ChangeFilterMenuVisibility()
    {
        _filterVisibility = !_filterVisibility;
        filterMenu.SetActive(_filterVisibility);
        heatMenu.SetActive(_filterVisibility);
    }

    public void ShowNewFilter(int index)
    {
        bool find = false;
        string filterName = "Filter_" + index;
        
        if (_filters.Count != 0)
        {
            foreach (GameObject filter in _filters)
            {
                filter.SetActive(false);
            }
            
            foreach (GameObject filter in _filters)
            {
                if (filter.name != "Filter_" + index)
                {
                    filter.SetActive(false);
                }
                else
                {
                    filter.SetActive(true);
                    find = true;
                }
            }
            if (find) { return; }
        }
        
        GameObject newFilter = InstantiateNewFilter(index); 
        _filters.Add(newFilter);
        
    }

    private GameObject InstantiateNewFilter(int index)
    {
        GameObject newFilter;

        if (_poiLabelTypes[index] == typeof(string))
        {
            List<string> categories = _utils.GetComponent<ProjectUtils>().GetAllCategoriesFromAttribute(index, _poiInfos);
         
            newFilter = Instantiate(checkGroupTemplate, filterMenu.transform);
            newFilter.GetComponent<RectTransform>().position = checkGroupTemplate.GetComponent<RectTransform>().position;
            newFilter.GetComponent<CheckGroupEffects>().UpdateCheckBoxes(categories);
            newFilter.GetComponent<CheckGroupEffects>().SetId(index);
        }
        else
        {
            Vector2 minMax = _utils.GetComponent<ProjectUtils>().GetMinMaxValueFromAttribute(index, _poiInfos);
        
            newFilter = Instantiate(minMaxSliderTemplate, filterMenu.transform);
            newFilter.GetComponent<RectTransform>().localPosition = minMaxSliderTemplate.GetComponent<RectTransform>().localPosition;
            newFilter.GetComponentInChildren<MinMaxSlider>().SetValues(minMax.x, minMax.y, minMax.x, minMax.y);
            newFilter.GetComponentInChildren<MinMaxSlider>().RefreshSliders();
            newFilter.GetComponent<BasicSliderEffects>().SetId(index);
        }

        newFilter.name = "Filter_" + index;
        newFilter.SetActive(true);
        
        return newFilter;
    }

    public void UpdatePoiDisplayed(string poiName, Transform parent)
    {
        _poiInfoDisplay.gameObject.SetActive(true);
        _poiInfoDisplay.GetComponentInChildren<Text>().text = "";

        GameObject tempManager = parent.name == "POIManager" ? _poiManager : _heatManager;
         
        List<string> labels = tempManager.GetComponent<DatasetReader>().GetDatabaseLabel();
        string[] poiInfos = tempManager.GetComponent<DatasetReader>().GetPoiInformation(poiName);
         
        for (int i = 0; i < poiInfos.Length; i++)
        {
            _poiInfoDisplay.GetComponentInChildren<Text>().text += labels[i] + ": " + poiInfos[i];
            if (poiInfos.Length - i > 1)
            {
                _poiInfoDisplay.GetComponentInChildren<Text>().text += ",\n";
            }
        } 
    }

    public void HidePoiInfo()
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

