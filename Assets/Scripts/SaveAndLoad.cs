using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

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
    void Awake()
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


    public void SaveData()
    {
        QuickSaveReader loaderNumberOfLevels = QuickSaveReader.Create("Level");
        int currentLevel = loaderNumberOfLevels.Read<int>("LevelNumber");
        if (currentLevel == null) currentLevel = 0;
        currentLevel += 1;
        QuickSaveWriter quickSaveWriterCurrentLevel = QuickSaveWriter.Create("CurrentLevelValues" + currentLevel);

        QuickSaveWriter quickSaveLevelWriter = QuickSaveWriter.Create("Level");

        foreach (Cube cube in cubes)
        {
            quickSaveWriterCurrentLevel.Write("CubePos " + cubes.IndexOf(cube), cube.transform.position);
            quickSaveWriterCurrentLevel.Write("CubePrefab " + cubes.IndexOf(cube), cube.prefabNum);

            int i = 0;
            foreach (Transform child in cube.transform)
            {

                CubeSide side = child.GetComponent<CubeSide>();
                quickSaveWriterCurrentLevel.Write("Cube " + (cubes.IndexOf(cube)) + " Side " + i, side.number);
                i++;

            }
        }

        quickSaveLevelWriter.Write("LevelNumber", currentLevel);

        quickSaveWriterCurrentLevel.Write("CubeCount", cubes.Count);
        quickSaveWriterCurrentLevel.Write("SidesCount", cubeSides.Count);
        quickSaveWriterCurrentLevel.Commit();
        quickSaveLevelWriter.Commit();
        Debug.Log("Saved data from level " + currentLevel);
    }
    public void LoadData()
    {
        QuickSaveReader loaderNumberOfLevels = QuickSaveReader.Create("Level");
        int currentLevel = loaderNumberOfLevels.Read<int>("LevelNumber");
        QuickSaveReader readerLevelValues = QuickSaveReader.Create("CurrentLevelValues" + currentLevel);

        int numberCubes = readerLevelValues.Read<int>("CubeCount");
        int numberOfSides = readerLevelValues.Read<int>("SidesCount");

        List<Vector3> loadedCubesPos = new List<Vector3>();
        List<int> loadedPrefabNumbers = new List<int>();
        List<int> loadedSidesValues = new List<int>();
        Debug.Log("Loaded data from level " + currentLevel);


        for (int i = 0; i < numberCubes; i++)

        {
            loadedPrefabNumbers.Add(readerLevelValues.Read<int>("CubePrefab " + i));
            loadedCubesPos.Add(readerLevelValues.Read<Vector3>("CubePos " + i));
            for (int j = 0; j < 6; j++)
            {
                loadedSidesValues.Add(readerLevelValues.Read<int>("Cube " + i + " Side " + j));

            }
        }

        ReloadObjectsData(numberCubes, numberOfSides, loadedCubesPos, loadedPrefabNumbers, loadedSidesValues); //NEEDS HEAVY REFACTOR
    }

    public void LoadData(int levelSelection)
    {
        Debug.Log("LevelSelection " + levelSelection);
        QuickSaveReader readerLevelValues = QuickSaveReader.Create("CurrentLevelValues" + levelSelection);

        int numberCubes = readerLevelValues.Read<int>("CubeCount");
        int numberOfSides = readerLevelValues.Read<int>("SidesCount");

        List<Vector3> loadedCubesPos = new List<Vector3>();
        List<int> loadedPrefabNumbers = new List<int>();
        List<int> loadedSidesValues = new List<int>();
        Debug.Log("Loaded data from level " + levelSelection);


        for (int i = 0; i < numberCubes; i++)

        {
            loadedPrefabNumbers.Add(readerLevelValues.Read<int>("CubePrefab " + i));
            loadedCubesPos.Add(readerLevelValues.Read<Vector3>("CubePos " + i));
            for (int j = 0; j < 6; j++)
            {
                loadedSidesValues.Add(readerLevelValues.Read<int>("Cube " + i + " Side " + j));

            }
        }

        ReloadObjectsData(numberCubes, numberOfSides, loadedCubesPos, loadedPrefabNumbers, loadedSidesValues); //NEEDS HEAVY REFACTOR
    }




    void ReloadObjectsData(int numberCubes, int numberOfSides, List<Vector3> loadedCubesPos, List<int> loadedPrefabNumbers, List<int> loadedSidesValues)
    {
        for (int i = 0; i < numberCubes; i++)
        {
            Vector3 cubePos = loadedCubesPos[i];
            int prefabNum = loadedPrefabNumbers[i];
            PickPrefabToPlace(prefabNum); //this picks the prefabs stored here relative to the int
            GameObject loadedCube = Instantiate(prefab, cubePos, Quaternion.identity);
            loadedCube.transform.SetParent(this.transform);
            cubes.Add(loadedCube.GetComponent<Cube>());

            for (int j = 0; j < 6; j++)
            {
                CubeSide side = loadedCube.transform.GetChild(j).GetComponent<CubeSide>();
                int sideValue = loadedSidesValues[j + (i * 6)];
                side.number = sideValue;


            }

            /* foreach (Transform child in loadedCube.transform)
             {
                 CubeSide side = child.GetComponent<CubeSide>();
                 if (cubeSides.Count < cubes.Count * 6) cubeSides.Add(side);
                 side.number = loadedSidesValues

             }*/

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




}
