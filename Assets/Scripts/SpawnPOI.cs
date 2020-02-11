using System;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;

public class SpawnPOI : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [Geocode]
    string[] _locationStrings;
    List<Vector2d> _locations = new List<Vector2d>();

    [SerializeField]
    GameObject _PoiPrefab;

    List<GameObject> _spawnedObjects;

    void Start()
    {
        GameObject poiManager = GameObject.Find("POIManager");
        List<string[]> Dataset = poiManager.GetComponent<DatasetReader>().GetPoiList();
        
        //_locations = new Vector2d[Dataset.Count];
        _spawnedObjects = new List<GameObject>();
        foreach (var poi in Dataset)
        {
            // poi[0] = id | poi[1] = lat | poi[2] = long | poi[3] = descrição | poi[4] em diante = dados opcionais
            string location = poi[1] + ", " + poi[2];
            _locations.Add(Conversions.StringToLatLon(location));
            
            var instance = Instantiate(_PoiPrefab, poiManager.transform);
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[_locations.Count-1]);
            instance.name = poi[0];
            instance.GetComponent<PoiClass>().myPoi.CreatePoi(poi[0], Convert.ToDouble(poi[1]), Convert.ToDouble(poi[2]), poi[3]);
            
            _spawnedObjects.Add(instance);
        }
    }
    
    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(1, 25, 1);
        }
    }
}
