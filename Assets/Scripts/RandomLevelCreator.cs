using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomLevelCreator : MonoBehaviour
{
    public int numberOfCubes = 2;
    public int sidesCombined = 2;
    public LevelSolver levelSolver;
    public LevelEditor levelEditor;
    public List<GameObject> cubesCreatedGO = new List<GameObject>();
    public List<Cube> cubesCreated = new List<Cube>();
    public List<CubeSide> cubeSidesCreated = new List<CubeSide>();
    public List<Vector2> sidesCreatedPositions = new List<Vector2>();



    public GameObject cubeContainer;
    public GameObject cubePrefab;
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
    public bool start;
    public bool running;
    public bool solved;

    public int[] positionOfCubes = {0, 0, 0, 0, 0, //0,0 is the center of the map (0,0)
                                    0, 0, 0, 0, 0,
                                    0, 0, 0, 0, 0, 
                                    0, 0, 0, 0, 0,
                                    0, 0, 0, 0, 0};


    private void Update()
    {
        cubeContainer = GameObject.Find("CubeContainer");
        if (!start)
        {
            return;
        }
        if (!solved && !running)
        {
            LevelGenerator(numberOfCubes, sidesCombined);
        }
        if (running)
        {
            if (levelSolver.solvedLevel)
            {
                solved = true;
            }
        }

    }
    public bool LevelGenerator(int nCubes, int nSamePos)
    {
        running = true;
        foreach (GameObject cubeGO in cubesCreatedGO)
        {
            Destroy(cubeGO);
        }
        cubesCreated.Clear();
        cubesCreatedGO.Clear();
        cubeSidesCreated.Clear();
        sidesCreatedPositions.Clear();

        List<int> intPos = new List<int>();
        //Random cube generator

        for (int i = 0; i < nCubes; i++)
        {
            intPos.Add(Random.Range(1, 26));
            Vector2 initialPos = GetPositionInWorldOfCubes(intPos[i]);
            PickPrefabToPlace(Random.Range(1, 21));
            GameObject newCube = Instantiate(cubePrefab, initialPos, Quaternion.identity, cubeContainer.transform);
            newCube.transform.Rotate(newCube.transform.forward, 90 * Random.Range(0, 4));
            cubesCreated.Add(newCube.GetComponent<Cube>());
            cubesCreatedGO.Add(newCube);
            newCube.GetComponent<Cube>().UpdateCube();
        }
        //Add sides
        foreach(Cube cube in cubesCreated)
        {
            foreach (Transform child in cube.transform)
            {
                cubeSidesCreated.Add(child.GetComponent<CubeSide>());
                sidesCreatedPositions.Add(child.position);
            }
        }
        //Check combined Sides
        int totalSharedPos = sidesCreatedPositions.GroupBy(_ => _).Where(_ => _.Count() > 1).Sum(_ => _.Count());
        if (totalSharedPos < nSamePos)
        {
            return LevelGenerator(nCubes,nSamePos);
        }

        //Check no cube without combined side
        //levelEditor.UpdateLevelLists();
        //levelEditor.CheckForDuplicates();
       
        //Init the list for the solver
        //levelSolver.cubes = cubesCreated;

        Invoke("StartSolver",1f);
        //levelSolver.StartSolution();

        return true;

    }
    void StartSolver()
    {
        levelEditor.CheckForDuplicates();
        foreach (Cube cube in cubesCreated)
        {
            int counter = 0;
            foreach (Transform child in cube.transform)
            {
                if (child.GetComponent<CubeSide>().combinedCube) counter++;
            }
            if (counter == 0)
            {
                LevelGenerator(numberOfCubes, sidesCombined);
                return;
            }
        }
        levelSolver.start = true;

    }

    public void StartLevelCreation()
    {
        start = true;
    }

    public Vector2 GetPositionInWorldOfCubes(int positionInArray)
    {
        Vector2 position;
        int row = positionInArray / 4;
        int column = positionInArray - row * 5;

        position.x = row - 3f;
        position.y = column + 3f;
        return position;
    }

    public void PickPrefabToPlace(int selection)
    {
        switch (selection)
        {
            case 1:
                cubePrefab = cube1;
                break;
            case 2:
                cubePrefab = cube2;
                break;
            case 3:
                cubePrefab = cube3;
                break;
            case 4:
                cubePrefab = cube4;
                break;
            case 5:
                cubePrefab = cube5;
                break;
            case 6:
                cubePrefab = cube6;
                break;
            case 7:
                cubePrefab = cube7;
                break;
            case 8:
                cubePrefab = cube8;
                break;
            case 9:
                cubePrefab = cube9;
                break;
            case 10:
                cubePrefab = cube10;
                break;
            case 11:
                cubePrefab = cube11;
                break;
            case 12:
                cubePrefab = cube12;
                break;
            case 13:
                cubePrefab = cube13;
                break;
            case 14:
                cubePrefab = cube14;
                break;
            case 15:
                cubePrefab = cube15;
                break;
            case 16:
                cubePrefab = cube16;
                break;
            case 17:
                cubePrefab = cube17;
                break;
            case 18:
                cubePrefab = cube18;
                break;
            case 19:
                cubePrefab = cube19;
                break;
            case 20:
                cubePrefab = cube20;
                break;
        }

    }
}
