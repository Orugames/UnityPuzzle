using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelCreator : MonoBehaviour
{
    public int numberOfCubes = 2;
    public int sidesCombined = 2;
    public GameObject cubePrefab;
    int startingCubePosInitial = 0;
    int startingCubePos = 0;
    public Vector2 initialCubePos;
    public Vector2 cubePos;
    public List<GameObject> cubesCreatedGO = new List<GameObject>();
    public List<Cube> cubesCreated = new List<Cube>();
    public List<CubeSide> cubeSidesCreated = new List<CubeSide>();


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


    public int[] positionOfCubes = {0, 0, 0, 0, 0, //0,0 is the center of the map (0,0)
                                    0, 0, 0, 0, 0,
                                    0, 0, 0, 0, 0, 
                                    0, 0, 0, 0, 0,
                                    0, 0, 0, 0, 0};


    public bool LevelGenerator(Vector2 firstPos, Vector2 secondPos)
    {
        startingCubePosInitial = Random.Range(0, 26);
        initialCubePos = GetPositionInWorldOfCubes(startingCubePosInitial);

        GameObject firstCube = Instantiate(cubePrefab, initialCubePos,Quaternion.identity);

        return false;



    }

    public void StartLevelCreation()
    {
        startingCubePosInitial = Random.Range(0, 26);
        startingCubePos = Random.Range(0, 26);
        bool levelCorrect = LevelGenerator(initialCubePos, cubePos);

        if (!levelCorrect) StartLevelCreation();
        

    }

    public Vector2 GetPositionInWorldOfCubes(int positionInArray)
    {
        Vector2 position;
        int row = positionInArray / 4;
        int column = positionInArray - row * 5;

        position.x = row;
        position.y = column;
        return position;
    }

    public GameObject PickPrefabToPlace(int selection)
    {
        GameObject prefab = new GameObject();
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
        return prefab;
    }
}
