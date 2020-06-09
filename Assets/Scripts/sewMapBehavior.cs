using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sewMapBehavior : MonoBehaviour
{
    private GameObject _mainMap;
    private GameObject _canvasHeader;
    private GameObject _poiManager;
    private GameObject _heatManager;

    private Scene _cenaAtiva;
    void Awake()
    {
        _mainMap = GameObject.Find("MainMap");
        _canvasHeader = GameObject.Find("CanvasHeader");
        _poiManager = GameObject.Find("POIManager");
        _heatManager = GameObject.Find("HeatManager");
        
        Vector2d latLon = GetComponent<CustomMapInfo>().GetLatLong();
        _poiManager.GetComponent<DatasetReader>().SetDatabasePath(GetComponent<CustomMapInfo>().GetDatabasePath());
        _mainMap.GetComponent<AbstractMap>().Initialize(latLon, 16);
        _canvasHeader.GetComponentInChildren<Text>().text = GetComponent<CustomMapInfo>().GetMapName();

        _poiManager.GetComponent<SpawnPOI>().InstatiatePoisInCustomMap();
    }
}
