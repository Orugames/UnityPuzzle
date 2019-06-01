using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using TMPro;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad instance = null;

    public List<Cube> cubes = new List<Cube>();
    public List<CubeSide> cubeSides = new List<CubeSide>();
    public List<GameObject> cubeSidesGO = new List<GameObject>();
    public GameObject cubeContainer;

    public GameObject prefab;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject cube10;
    public GameObject cube11;
    public GameObject cube12;
    public GameObject cube13;
    public GameObject cube14;
    public GameObject cube15;
    public GameObject cube16;
    public GameObject cube17;
    public GameObject cube18;
    public GameObject cube19;
    public GameObject cube20;
    GameObject cubeSelected;

    public bool prefabPicked;
    public bool singletonEnabled = true;
    public int levelSelected = 0;

    void Awake()
    {
        if (singletonEnabled)
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadData(int levelSelection)
    {
        cubeContainer = GameObject.Find("CubeContainer");
        int currentLevel = levelSelection;
        /*if (ES3.KeyExists("MaxLevelsCreated"))
        {
            currentLevel = ES3.Load<int>("MaxLevelsCreated");
        }
        else
        {
            ES3.Save<int>("MaxLevelsCreated", currentLevel = 0);
        }*/

        int nCubes = ES3.Load<int>("NumberCubesLevel" + currentLevel);

        for (int i = 0; i < nCubes; i++)
        {
            int prefabNum = ES3.Load<int>("Cube" + i + "Level" + currentLevel + "Prefab");
            PickPrefabToPlace(prefabNum);
            prefabPicked = false;
            GameObject newCubeGO = Instantiate(prefab, cubeContainer.transform);
            ES3.LoadInto<Cube>("Cube" + i + "Level" + currentLevel, newCubeGO.GetComponent<Cube>()); //we load the data onto a new cube component
            newCubeGO.transform.position = newCubeGO.GetComponent<Cube>().position;

            for (int j = 0; j < 6; j++)
            {

                ES3.LoadInto<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + currentLevel, newCubeGO.transform.GetChild(j).gameObject.GetComponent<CubeSide>());
            }


        }
        Debug.Log("Loaded level " + currentLevel);

    }



    public void PickPrefabToPlace(int selection)
    {
        switch (selection)
        {
            case 1:
                prefab = cube1;
                break;
            case 2:
                prefab = cube2;
                break;
            case 3:
                prefab = cube3;
                break;
            case 4:
                prefab = cube4;
                break;
            case 5:
                prefab = cube5;
                break;
            case 6:
                prefab = cube6;
                break;
            case 7:
                prefab = cube7;
                break;
            case 8:
                prefab = cube8;
                break;
            case 9:
                prefab = cube9;
                break;
            case 10:
                prefab = cube10;
                break;
            case 11:
                prefab = cube11;
                break;
            case 12:
                prefab = cube12;
                break;
            case 13:
                prefab = cube13;
                break;
            case 14:
                prefab = cube14;
                break;
            case 15:
                prefab = cube15;
                break;
            case 16:
                prefab = cube16;
                break;
            case 17:
                prefab = cube17;
                break;
            case 18:
                prefab = cube18;
                break;
            case 19:
                prefab = cube19;
                break;
            case 20:
                prefab = cube20;
                break;
        }
        prefabPicked = true;
    }




}
