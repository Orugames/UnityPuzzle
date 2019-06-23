using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using TMPro;

public class SaveAndLoad : MonoBehaviour
{
    public static SaveAndLoad instance = null;

    public List<Cube> cubes = new List<Cube>();
    public List<GameObject> cubesGO = new List<GameObject>();
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

        int nCubes = ES3.Load<int>("NumberCubesLevel" + currentLevel);

        for (int i = 0; i < nCubes; i++)
        {
            int prefabNum = ES3.Load<int>("Cube" + i + "Level" + currentLevel + "Prefab");
            PickPrefabToPlace(prefabNum);
            prefabPicked = false;
            GameObject newCubeGO = Instantiate(prefab, cubeContainer.transform);
            cubesGO.Add(newCubeGO);
            ES3.LoadInto<Cube>("Cube" + i + "Level" + currentLevel, newCubeGO.GetComponent<Cube>()); //we load the data onto a new cube component
            cubes.Add(newCubeGO.GetComponent<Cube>());
            newCubeGO.transform.position = newCubeGO.GetComponent<Cube>().position;
            newCubeGO.transform.rotation = newCubeGO.GetComponent<Cube>().rotation;

            for (int j = 0; j < 6; j++)
            {
                CubeSide newCubeSide = newCubeGO.transform.GetChild(j).gameObject.GetComponent<CubeSide>();
                cubeSides.Add(newCubeSide);
                ES3.LoadInto<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + currentLevel, newCubeSide);
                newCubeSide.numberText.transform.Rotate(newCubeSide.transform.forward, -90 * (newCubeGO.transform.eulerAngles.z / 90));
            }


        }
        Debug.Log("Loaded level " + currentLevel);

    }
    public void LoadData(LevelButtonData levelButtonData)
    {
        GameObject levelMenuParent = levelButtonData.parent;
        int levelSelected = 1;
        if (levelMenuParent.name == "List Item")
        {
            levelSelected = levelButtonData.level;
        }
        else
        {
            int multiplierForEveryMenu = int.Parse(levelMenuParent.name.Substring(levelMenuParent.name.Length - 1));
            levelSelected = 10 + 20 * (multiplierForEveryMenu - 1) + levelButtonData.level;
            Debug.Log(levelSelected);
        }

        cubeContainer = GameObject.Find("CubeContainer");
        int currentLevel = levelSelected;

        int nCubes = ES3.Load<int>("NumberCubesLevel" + currentLevel);

        for (int i = 0; i < nCubes; i++)
        {
            int prefabNum = ES3.Load<int>("Cube" + i + "Level" + currentLevel + "Prefab");
            PickPrefabToPlace(prefabNum);
            prefabPicked = false;
            GameObject newCubeGO = Instantiate(prefab, cubeContainer.transform);
            cubesGO.Add(newCubeGO);
            ES3.LoadInto<Cube>("Cube" + i + "Level" + currentLevel, newCubeGO.GetComponent<Cube>()); //we load the data onto a new cube component
            cubes.Add(newCubeGO.GetComponent<Cube>());
            newCubeGO.transform.position = newCubeGO.GetComponent<Cube>().position;
            newCubeGO.transform.rotation = newCubeGO.GetComponent<Cube>().rotation;

            for (int j = 0; j < 6; j++)
            {
                CubeSide newCubeSide = newCubeGO.transform.GetChild(j).gameObject.GetComponent<CubeSide>();
                cubeSides.Add(newCubeSide);
                ES3.LoadInto<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + currentLevel, newCubeSide);
                newCubeSide.numberText.transform.Rotate(newCubeSide.transform.forward, -90 * (newCubeGO.transform.eulerAngles.z / 90));
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

    public void CombineSides()
    {
        for (var i = 0; i < cubeSides.Count - 1; i++)
        {
            for (var k = i + 1; k < cubeSides.Count; k++)
            {

                var distance = Vector3.Distance(cubeSides[i].transform.position, cubeSides[k].transform.position);

                if (distance < 0.1f)
                {
                    if (cubeSides[i].cubeParent.chosenMarker == cubeSides[k].cubeParent.chosenMarker) //if the combined sides have the same side coloured
                    {
                        cubeSides[i].cubeParent.RandomChosenMarker();
                        cubeSides[k].cubeParent.RandomChosenMarker();
                    }
                    Debug.Log("Found duplicate, change to two combined ones");
                    Debug.Log(cubeSides[i].name);
                    Debug.Log(cubeSides[k].name);


                    cubeSides[i].combinedCube = true;
                    cubeSides[k].combinedCube = true;
                    cubeSides[i].cubeParents.Add(cubeSides[i].gameObject.transform.parent.GetComponent<Cube>());
                    cubeSides[i].cubeParents.Add(cubeSides[k].transform.parent.GetComponent<Cube>());
                    cubeSides[k].cubeParents.Add(cubeSides[i].transform.parent.GetComponent<Cube>());
                    cubeSides[k].cubeParents.Add(cubeSides[k].transform.parent.GetComponent<Cube>());
                    cubeSides[i].similarCubeSides.Add(cubeSides[k]);
                    cubeSides[k].similarCubeSides.Add(cubeSides[i]);

                    cubeSides[k].GetComponent<BoxCollider>().enabled = false;
                    cubeSides[k].numberText.enabled = false;
                    cubeSides[k].spriteParent.SetActive(false);

                    cubeSides[i].UpdateSide();
                    cubeSides[k].UpdateSide();



                }
            }
        }
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }


}
