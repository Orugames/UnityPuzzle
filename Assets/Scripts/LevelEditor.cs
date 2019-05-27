using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : MonoBehaviour
{
    float gridSize = 1;

    public  GameObject prefab;
    public  GameObject cube1;
    public  GameObject cube2;
    public  GameObject cube3;
    public  GameObject cube4;
    public  GameObject cube5;
    public  GameObject cube6;
    public  GameObject cube7;
    public  GameObject cube8;
    public  GameObject cube9;
    public  GameObject cube10;
    public  GameObject cube11;
    public  GameObject cube12;
    public  GameObject cube13;
    public  GameObject cube14;
    public  GameObject cube15;
    public  GameObject cube16;
    public  GameObject cube17;
    public  GameObject cube18;
    public  GameObject cube19;
    public  GameObject cube20;

    public bool modifyValues;
    public bool prefabPicked;
    public bool prefabTransform;
    public bool prefabRotate;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnMouseDown()
    {
        if (!prefabPicked)
        {

        }
        else //prefabReadyToPick
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
                    prefabPicked = false;
                }
            }

        }

    }

    public void PickPrefabToPlace(int selection)
    {
        switch(selection)
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
        prefabTransform = true;
    }
    public void RotateCube()
    {
        prefabRotate = true;
    }
}
