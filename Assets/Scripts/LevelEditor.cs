using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Linq;
using CI.QuickSave;
using TMPro;

public class LevelEditor : MonoBehaviour
{
    float gridSize = 1;

    public GameObject combinedSidePrefab;
    public GameObject cubeContainer;
    public GameObject saveAndLoad;
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
    public TextMeshProUGUI levelText;
    GameObject cubeSelected;
    public List<Cube> cubes = new List<Cube>();
    public List<CubeSide> cubeSides = new List<CubeSide>();
    public List<GameObject> cubeSidesGO = new List<GameObject>();
    public bool modifyValuesBool;
    public bool prefabPicked;
    public bool prefabTransform;
    public bool prefabReadyToTransform;
    public bool prefabRotate;
    public bool prefabReadyToRotate;
    public bool prefabErase;
    public bool initBool;
    public int levelSelected;
    public List<GameObject> cubesGO = new List<GameObject>();

    private void Start()
    {
        //Init();
    }
    public void Init()
    {
        /*saveAndLoad = GameObject.FindWithTag("Controllers");
        levelSelected = saveAndLoad.GetComponent<SaveAndLoad>().levelSelected;

        levelText.text = "Level " + levelSelected;
        if (levelSelected == 0) levelText.text = "New Level";*/


        initBool = true;
        //Invoke("AssingToOriginalParent",1);
        //TransferAllA2B(saveAndLoad.transform, cubeContainer.transform);
    }

    void TransferAllA2B(Transform a, Transform b)
    {
        bool WorldPositionStayTheSame = false;

        Transform kid;
        /*while (kid = a.GetChild(0))
        {         
            kid.SetParent(b, WorldPositionStayTheSame);
            cubes.Add(kid.GetComponent<Cube>());

        }*/
        for (int i = a.childCount - 1; i >= 0; --i)
        {
            Transform child = a.GetChild(i);
            Debug.Log("moving object: " + child.name);
            child.SetParent(b.transform, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!initBool) Init();


        //if (cubes == null || cubeSides == null)
        //{
        //    return;
        //}

        if (GameObject.Find("CubeContainer") == null)
        {
            cubeContainer = new GameObject();
            cubeContainer.name = "CubeContainer";
        }
        else cubeContainer = GameObject.Find("CubeContainer");


        cubes.Clear();
        cubeSides.Clear();
        cubeSidesGO.Clear();
        foreach (Transform child in cubeContainer.transform)
        {
            Cube cube = child.GetComponent<Cube>();
            cubes.Add(cube);
            cubesGO.Add(child.gameObject);
        }
        OnMouseDown();


        foreach (Cube cube in cubes)
        {
            cube.UpdateCube();
            cube.CheckCompletion();

            foreach (Transform child in cube.transform)
            {
                cubeSides.Add(child.GetComponent<CubeSide>());


            }
        }
        cubeSides = cubeSides.Distinct().ToList();

        foreach (CubeSide side in cubeSides)
        {
            cubeSidesGO.Add(side.gameObject);
        }
        cubeSides.TrimExcess();
        cubes.TrimExcess();
        CheckForDuplicates();



    }

    public void OnMouseDown()
    {
        if (!prefabPicked && !prefabTransform && !prefabRotate)
        {

        }
        else if (prefabPicked)
        {
            if (prefab == null)
            {
                Debug.Log("Warning, no prefab");
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GameObject newCube = Instantiate(prefab);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 rounded;
                    rounded.x = Mathf.RoundToInt((hit.point.x * gridSize) / gridSize);
                    rounded.y = Mathf.RoundToInt((hit.point.y * gridSize) / gridSize);
                    rounded.z = 0;
                    newCube.transform.position = rounded;
                    newCube.transform.parent = cubeContainer.transform;

                    cubes.Add(newCube.GetComponent<Cube>());
                    /*foreach (Transform child in newCube.transform)
                    {
                        cubeSides.Add(child.GetComponent<CubeSide>());
                    }*/
                    prefabPicked = false;

                }
            }

        }

        if (prefabTransform) //Pushed the button to transform, ready to pick a cube
        {
            TransformCubeLogic();
        }
        if (prefabRotate)
        {
            RotateCubeLogic();
        }
        if (prefabErase)
        {
            EraseCubeLogic();
        }

    }


    public void SaveData()
    {
        int currentLevel = 0;
        if (ES3.KeyExists("MaxLevelsCreated"))
        {
            currentLevel = ES3.Load<int>("MaxLevelsCreated");
        }
        else
        {
            ES3.Save<int>("MaxLevelsCreated",currentLevel = 0);
        }
        currentLevel += 1;

        //ES3.Save<List<Cube>>("CubeList" + currentLevel, cubes);
        //ES3.Save<List<CubeSide>>("CubeSides" + currentLevel,cubeSides);
        ES3.Save<int>("MaxLevelsCreated", currentLevel);
        //ES3.Save<List<GameObject>>("CubesGO" + currentLevel,cubesGO);

        for (int i = 0; i < cubeContainer.transform.childCount; i++)
        {
            Transform child = cubeContainer.transform.GetChild(i);
            ES3.Save<Cube>("Cube" + i + "Level" + currentLevel,child.GetComponent<Cube>()); //save each cube data
            ES3.Save<int>("Cube" + i + "Level" + currentLevel + "Prefab",child.GetComponent<Cube>().prefabNum);

            for (int j = 0; j < child.transform.childCount ; j++) //moving on the children of each cube
            {
                Transform grandchild = child.transform.GetChild(j);

                ES3.Save<CubeSide>("CubeSide" + j + "Cube" + i + "Level" + currentLevel, grandchild.GetComponent<CubeSide>());
            }
            ES3.Save<int>("NumberCubesLevel" + currentLevel, cubeContainer.transform.childCount);

        }
       


        Debug.Log("Saved data from level " + currentLevel);

        levelText.text = "Saved level " + currentLevel;
    }
    public void LoadData()
    {
        int currentLevel = 0;
        if (ES3.KeyExists("MaxLevelsCreated"))
        {
            currentLevel = ES3.Load<int>("MaxLevelsCreated");
        }
        else
        {
            ES3.Save<int>("MaxLevelsCreated", currentLevel = 0);
        }
        
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
        
    }

    public void LoadData(int levelSelection)
    {
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

    }



    void HologramCubeTransform()
    {
        if (prefabReadyToTransform)
        {
            //If we clicked a cube, we see and move its hologram
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name is "LevelEditor")
                {
                    return;
                }
                cubeSelected = hit.collider.gameObject.transform.parent.gameObject; //we select the cube parent of this side
                Debug.Log(cubeSelected.name);
                Vector3 rounded;
                rounded.x = Mathf.RoundToInt((hit.point.x * gridSize) / gridSize);
                rounded.y = Mathf.RoundToInt((hit.point.y * gridSize) / gridSize);
                rounded.z = 0;
                cubeSelected.transform.position = rounded;

            }
        }
    }

    void TransformCubeLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!prefabReadyToTransform) //input to select a cube
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name is "LevelEditor")
                    {
                        return;
                    }
                    cubeSelected = hit.collider.gameObject.transform.parent.gameObject; //we select the cube parent of this side
                    Debug.Log(cubeSelected.name);
                    /*Vector3 rounded;
                    rounded.x = Mathf.RoundToInt((hit.point.x * gridSize) / gridSize);
                    rounded.y = Mathf.RoundToInt((hit.point.y * gridSize) / gridSize);
                    rounded.z = 0;
                    cubeSelected.transform.position = rounded;*/
                    prefabReadyToTransform = true;
                }
            }
            else //Then we move that cube again
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 rounded;
                    rounded.x = Mathf.RoundToInt((hit.point.x * gridSize) / gridSize);
                    rounded.y = Mathf.RoundToInt((hit.point.y * gridSize) / gridSize);
                    rounded.z = 0;
                    cubeSelected.transform.position = rounded;
                    //prefabTransform = false;
                    prefabReadyToTransform = false;
                    cubeSelected.name = "Cube " + cubeSelected.transform.position.x.ToString() +
                                        " , " + cubeSelected.transform.position.y.ToString();

                }
            }

        }
    }
    void RotateCubeLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!prefabReadyToRotate) //input to select a cube
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name is "LevelEditor")
                    {
                        return;
                    }
                    cubeSelected = hit.collider.gameObject.transform.parent.gameObject; //we select the cube parent of this side
                    Debug.Log(cubeSelected.name);
                    prefabReadyToRotate = true;
                }
            }
            else //Then we rotate that cube 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    cubeSelected.transform.Rotate(cubeSelected.transform.forward, 90);
                    //prefabRotate = false;
                    prefabReadyToRotate = false;
                }
            }

        }
    }


    private void CheckForDuplicates()
    {
        for (var i = 0; i < cubeSides.Count - 1; i++)
        {
            for (var k = i + 1; k < cubeSides.Count; k++)
            {
                var distance = Vector3.Distance(cubeSides[i].transform.position, cubeSides[k].transform.position);

                if (distance < 0.1f && !cubeSides[i].combinedCube && !cubeSides[k].combinedCube)
                {
                    Debug.Log("Found duplicate, change to two combined ones");
                    Debug.Log(cubeSides[i].name);
                    Debug.Log(cubeSides[k].name);

                    cubeSides[i].combinedCube = true;
                    cubeSides[k].combinedCube = true;
                    cubeSides[i].cubeParents.Add(cubeSides[i].gameObject.transform.parent.GetComponent<Cube>());
                    cubeSides[i].cubeParents.Add(cubeSides[k].transform.parent.GetComponent<Cube>());
                    cubeSides[k].cubeParents.Add(cubeSides[i].transform.parent.GetComponent<Cube>());
                    cubeSides[k].cubeParents.Add(cubeSides[k].transform.parent.GetComponent<Cube>());
                    cubeSides[i].similarCubeSide = cubeSides[k];
                    cubeSides[k].similarCubeSide = cubeSides[i];

                    cubeSides[k].GetComponent<MeshRenderer>().enabled = false;
                    cubeSides[k].GetComponent<BoxCollider>().enabled = false;



                }
            }
        }
    }

    public int GetRepetitions(string[] myList, string value)
    {
        int repetitions = 0;
        for (int i = 0; i < myList.Length; i++)
        {
            if ((myList[i] == value) && ((i == 0 ? false : myList[i - 1] == value) ||
                 (i == myList.Length - 1 ? false : myList[i + 1] == value)))
            {
                repetitions++;
            }
        }
        return repetitions;
    }

    public void ModifyValuesToggle()
    {
        modifyValuesBool = !modifyValuesBool;
        foreach (CubeSide cubeSide in cubeSides)
        {
            cubeSide.modifyValues = modifyValuesBool;
        }
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

    public void TransformCube()
    {
        prefabTransform = !prefabTransform;
    }
    public void RotateCube()
    {
        prefabRotate = !prefabRotate;
    }
    public void EraseCube()
    {
        prefabErase = !prefabErase;
    }
    public void GoToLobby()
    {
        foreach (Transform child in cubeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(GameObject.FindWithTag("Controllers"));
        SceneManager.LoadScene(0);
    }
    public void ClearStage()
    {
        cubes.Clear();
        cubeSides.Clear();
        foreach (Transform child in cubeContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void EraseCubeLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name is "LevelEditor")
                {
                    return;
                }
                cubeSelected = hit.collider.gameObject.transform.parent.gameObject; //we select the cube parent of this side
                Debug.Log(cubeSelected.name);
                cubes.Remove(cubeSelected.GetComponent<Cube>());
                Destroy(cubeSelected);

            }


        }
    }
}