using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CI.QuickSave;
using System.Collections;
using System.IO;
using ES3Internal;
using TMPro;

public class LobbyLogic : MonoBehaviour
{
    string levelSelected;
    public GameObject gridParent;
    public GameObject buttonPrefab;
    public List<GameObject> levelsButtons = new List<GameObject>();
    void Start()
    {
        levelsButtons.Clear();
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
            levelSelectionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i+1);
            levelsButtons.Add(levelSelectionButton);
            levelSelectionButton.name = (i+1).ToString();
            Button b = levelSelectionButton.GetComponent<Button>();

            Button deleteButton = levelSelectionButton.transform.GetChild(1).GetComponent<Button>();
            int x;
            x = i+1;
            
            deleteButton.onClick.AddListener(delegate () { DestroyLevel(x.ToString()); }); //BUG of delegates

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
    public void DestroyLevel(string levelNumberString)
    {
        //We delete each entry of a saved level
        int levelNumber = int.Parse(levelNumberString); //BUG
        Debug.Log("Destroy level " + levelNumber);

        int nCubes = ES3.Load<int>("NumberCubesLevel" + levelNumber);

        for (int i = 0; i < nCubes; i++)
        {
            ES3.DeleteKey("Cube" + i + "Level" + levelNumber + "Prefab");
            ES3.DeleteKey("Cube" + i + "Level" + levelNumber);
            for (int j = 0; j < 6; j++)
            {

                ES3.DeleteKey("CubeSide" + j + "Cube" + i + "Level" + levelNumber);
            }
        }
        ES3.DeleteKey("NumberCubesLevel" + levelNumber);

        //We -1 the maxlevels created so we have one button less
        int maxLevels = ES3.Load<int>("MaxLevelsCreated");
        maxLevels -= 1;
        ES3.Save<int>("MaxLevelsCreated",maxLevels);

        GameObject buttonToRemoveGO = levelsButtons[levelNumber];
        levelsButtons.Remove(levelsButtons[levelNumber]);
        levelsButtons.TrimExcess();
        Destroy(buttonToRemoveGO);
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
}