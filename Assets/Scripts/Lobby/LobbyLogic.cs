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
        Debug.Log("Maxlevels" + maxLevels);
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
            
            deleteButton.onClick.AddListener(delegate () { UpdateSaveLevels(x); }); //BUG of delegates

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

        SaveAndLoad.instance.levelSelected = levelNumber;

        SaveAndLoad.instance.LoadData(levelNumber);
        //SaveAndLoad.instance.singletonEnabled = false;
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
  
        
    }
    public void UpdateSaveLevels(int LevelToDelete)
    {

        Debug.Log("Level to delete " + LevelToDelete);
        int maxLevels = 0;
        if (ES3.KeyExists("MaxLevelsCreated"))
        {
            maxLevels = ES3.Load<int>("MaxLevelsCreated");
        }
        else
        {
            ES3.Save<int>("MaxLevelsCreated", maxLevels = 0);
        }

        int nCubes;
        GameObject buttonToRemoveGO;
        if (LevelToDelete == maxLevels) //if we destroy the last level on the list
        {
            Debug.Log("Nivel elegido para borrar es el ultimo");

            DeleteEndOfSavedLevels(maxLevels);

            buttonToRemoveGO = levelsButtons[maxLevels-1];
            levelsButtons.Remove(levelsButtons[maxLevels-1]);
            levelsButtons.TrimExcess();
            Destroy(buttonToRemoveGO);

            foreach (Transform child in gridParent.transform)
            {
                Destroy(child.gameObject);
            }
            Start();
            return;
        }

        Debug.Log("Nivel elegido para borrar no es el ultimo, se actualiza la lista");
        for (int k = LevelToDelete; k < maxLevels; k++)
        {
            nCubes = ES3.Load<int>("NumberCubesLevel" + (k+1));
            ES3.Save<int>("NumberCubesLevel" + k, nCubes);

            for (int i = 0; i < nCubes; i++)
            {
                int prefabNum = ES3.Load<int>("Cube" + i + "Level" + (k + 1) + "Prefab");
                ES3.Save<int>("Cube" + i + "Level" + k + "Prefab", prefabNum);

                Cube newCube = ES3.Load<Cube>("Cube" + i + "Level" + (k + 1));
                ES3.Save<Cube>("Cube" + i + "Level" + k, newCube);

                for (int j = 0; j < 6; j++)
                {

                    CubeSide newCubeSide = ES3.Load<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + (k + 1));
                    ES3.Save<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + k, newCubeSide);
                }
            }

        }
        //Method to trim the end of the saveGames
        DeleteEndOfSavedLevels(maxLevels);

        buttonToRemoveGO = levelsButtons[LevelToDelete];
        levelsButtons.Remove(levelsButtons[LevelToDelete]);
        levelsButtons.TrimExcess();
        Destroy(buttonToRemoveGO);

        foreach(Transform child in gridParent.transform)
        {
            Destroy(child.gameObject);
        }
        Start();

        /*int nCubes = ES3.Load<int>("NumberCubesLevel" + 3 + 1);
        ES3.Save<int>("NumberCubesLevel" + 3, nCubes);

        for (int i = 0; i < nCubes; i++)
        {
            int prefabNum = ES3.Load<int>("Cube" + i + "Level" + 3 + 1 + "Prefab");
            ES3.Save<int>("Cube" + i + "Level" + 3 + "Prefab", prefabNum);

            Cube newCube = ES3.Load<Cube>("Cube" + i + "Level" + 3 + 1);
            ES3.Save<Cube>("Cube" + i + "Level" + 3, newCube);

            for (int j = 0; j < 6; j++)
            {

                CubeSide newCubeSide = ES3.Load<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + 3 + 1);
                ES3.Save<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + 3, newCubeSide);
            }
        }*/


    }


    void DeleteEndOfSavedLevels(int maxLevels)
    {
        int nCubes = 0;
        nCubes = ES3.Load<int>("NumberCubesLevel" + maxLevels);
        for (int i = 0; i < nCubes; i++)
        {
            ES3.DeleteKey("Cube" + i + "Level" + maxLevels);
            ES3.DeleteKey("Cube" + i + "Level" + maxLevels + "Prefab");

            for (int j = 0; j < 6; j++)
            {
                ES3.DeleteKey("CubeSide" + j + "Cube" + i + "Level" + maxLevels);

            }
        }
        ES3.DeleteKey("NumberCubesLevel" + maxLevels);
        int maxLevelsNew = ES3.Load<int>("MaxLevelsCreated");
        maxLevelsNew -= 1;
        ES3.Save<int>("MaxLevelsCreated", maxLevelsNew);

    }

}