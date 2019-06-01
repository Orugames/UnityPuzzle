using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CI.QuickSave;
using System.Collections;
using System.IO;
using ES3Internal;

public class LobbyLogic : MonoBehaviour
{
    string levelSelected;
    public GameObject gridParent;
    public GameObject buttonPrefab;

    void Start()
    {
        int maxLevels = 0;
        if (ES3.KeyExists("MaxLevelsCreated"))
        {
            maxLevels = ES3.Load<int>("MaxLevelsCreated");
        }
        else
        {
            ES3.Save<int>("MaxLevelsCreated", maxLevels = 0);
        }
        
        for (int i = 0; i < maxLevels; i++)
        {
            GameObject levelSelectionButton = Instantiate(buttonPrefab);
            levelSelectionButton.SetActive(true);
            levelSelectionButton.transform.SetParent(gridParent.transform,false);
            levelSelectionButton.GetComponent<RectTransform>().localScale = Vector3.one;
            levelSelectionButton.GetComponentInChildren<Text>().text = "Level " + (i+1);
            levelSelectionButton.name = (i+1).ToString();
            Button b = levelSelectionButton.GetComponent<Button>();
            int x = new int();
            x = i+1;
            b.onClick.AddListener(delegate () { LoadCreatedLevel(x); });


        }
    }


    public void LoadNewLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCreatedLevel(int levelNumber)
    {
        Debug.Log("Load level " + levelNumber);
        SceneManager.LoadScene(1);

        SaveAndLoad.instance.LoadData(levelNumber);
        SaveAndLoad.instance.singletonEnabled = false;
        this.enabled = false;

    }
    public void DestroyLevel(GameObject button)
    {
        int level = int.Parse(button.name);
        
        
        //File.Delete()
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
}