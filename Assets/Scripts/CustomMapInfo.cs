using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomMapInfo : MonoBehaviour
{
    private GameObject _getLatitude;
    private GameObject _getLongitude;
    private GameObject _getDatabase;
    private GameObject _getName;

    private string _latitude;
    private string _longitude;
    private string _databasePath;
    private string _mapName;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _getLatitude = GameObject.Find("FLD_Lat");
            _getLongitude = GameObject.Find("FLD_Long");
            _getDatabase = GameObject.Find("FLD_Path1");
            _getName = GameObject.Find("FLD_MapName");
            
            _getLatitude.GetComponent<InputField>().text = "40.7617784";
            _getLongitude.GetComponent<InputField>().text = "-73.9792788";
            _getDatabase.GetComponent<InputField>().text = "E:/Documentos/ny.csv";
            _getName.GetComponent<InputField>().text = "Manhattan - New York City";
            
            _latitude = _getLatitude.GetComponent<InputField>().text;
            _longitude = _getLongitude.GetComponent<InputField>().text;
            _databasePath = _getDatabase.GetComponent<InputField>().text;
            _mapName = _getName.GetComponent<InputField>().text;
        }
    }

    public void CreateNewMap()
    {
        DontDestroyOnLoad(gameObject);

        if (Double.TryParse(_latitude, out double a) && Double.TryParse(_longitude, out double b))
        {
            if (_latitude != null || _longitude != null || _databasePath != null || _mapName != null)
            {
                Debug.Log("New Map Created - Loading...");
                SceneManager.LoadScene("Assets/Scenes/MainScenes/CustomScene.unity", LoadSceneMode.Single);

                return;
            }
        }
        
        print("Certifique-se de que todos os parâmetros foram passados corretamente!");
    }

    public Vector2d GetLatLong()
    {
        float lat = Single.Parse(_latitude, CultureInfo.InvariantCulture);
        float lon = Single.Parse(_longitude, CultureInfo.InvariantCulture);
        
        Vector2d latLong = new Vector2d(lat, lon);
        return latLong;
    }
    
    public string GetMapName()
    {
        return _mapName;
    }
    
    public string GetDatabasePath()
    {
        return _databasePath;
    }
    
}
