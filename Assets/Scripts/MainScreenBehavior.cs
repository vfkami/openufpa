using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenBehavior : MonoBehaviour
{
    public Button defaultMap;
    public Button customMap;
    public Button btnAbout;

    public GameObject MainScreen;
    public GameObject CustomMapScreen;
    public GameObject AboutScreen;
    
    public void LoadDefaultScene()
    {
        SceneManager.LoadScene("Assets/Scenes/MainScenes/DefaultScene.unity", LoadSceneMode.Single);
    }

    public void WakeAboutScreen()
    {
        MainScreen.SetActive(false);
        AboutScreen.SetActive(true);

    }
    
    public void WakeCustomMapScreen()
    {
        MainScreen.SetActive(false);
        CustomMapScreen.SetActive(true);

    }
    
    public void BackButtonCall()
    {
        CustomMapScreen.SetActive(false);
        AboutScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}
