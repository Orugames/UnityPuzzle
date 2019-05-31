using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CI.QuickSave;

public class LobbyLogic : MonoBehaviour
{
    string levelSelected;
    public GameObject gridParent;
    public GameObject buttonPrefab;

    void Start()
    {
        QuickSaveReader loaderNumberOfLevels = QuickSaveReader.Create("Level");
        int currentLevel = loaderNumberOfLevels.Read<int>("LevelNumber");
        for (int i = 0; i < currentLevel; i++)
        {
            GameObject levelSelectionButton = Instantiate(buttonPrefab);
            levelSelectionButton.transform.parent = gridParent.transform;
            levelSelectionButton.GetComponent<RectTransform>().localScale = Vector3.one;
            levelSelectionButton.GetComponentInChildren<Text>().text = "Level " + i;
            Button b = levelSelectionButton.GetComponent<Button>();
            int x = new int();
            x = i;
            b.onClick.AddListener(delegate () { LoadCreatedLevel(x); });


        }
    }


    public void LoadNewLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCreatedLevel(int levelNumber)
    {
        Debug.Log("cargando info del nivel " + levelNumber);
        SceneManager.LoadScene(1);
        SaveAndLoad.instance.LoadData(levelNumber);
        SaveAndLoad.instance.singletonEnabled = false;
        this.enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void SaveData()
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
    }*/
}
