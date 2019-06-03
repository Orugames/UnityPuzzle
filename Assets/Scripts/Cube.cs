using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Color color;
    public Vector2 position;
    public Quaternion rotation;
    public bool cubeCompleted;
    public GameObject prefab;
    public int prefabNum;
    public List<CubeSide> cubeSides = new List<CubeSide>();
    public List<int> cubeSidesNumbers = new List<int>();
    public List<Color> presetColors = new List<Color> { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow };


    public void Start()
    {
        InitColor();

        foreach (Transform child in transform)
        {
            cubeSides.Add(child.GetComponent<CubeSide>());


            child.GetComponent<CubeSide>().cubeSideColor = color;
        }
        name = "Cube " + transform.position.x.ToString() + " , " + transform.position.y.ToString();

    }

    private void InitColor()
    {
        if (color == transform.GetChild(0).GetComponent<CubeSide>().cubeSideColor) //here we init the color
        {
            color = new Color();

            color = presetColors[UnityEngine.Random.Range(0, presetColors.Count)];
        }
    }

  

    public void UpdateCube()
    {
        position = transform.position;
        rotation = transform.rotation;
        cubeSidesNumbers.Clear();
        cubeSides.Clear();
        foreach (Transform child in transform)
        {
            cubeSides.Add(child.GetComponent<CubeSide>());            

            child.GetComponent<CubeSide>().cubeSideColor = color;
        }
        foreach (CubeSide cubeSide in cubeSides)
        {
            cubeSide.UpdateSide();
            cubeSidesNumbers.Add(cubeSide.number);

        }      
        cubeSidesNumbers.Sort();

        /*Debug.Log(String.Join("",
             new List<int>(cubeSidesNumbers)
             .ConvertAll(i => i.ToString())
             .ToArray()));*/

        cubeCompleted = CheckCompletion();

    }


    public bool CheckCompletion()
    {
        foreach (CubeSide side in cubeSides)
        {
            if (side.number == 0 || side.number < 0 || side.number > 6 || cubeSides.Count != 6)
            {
                //Debug.Log("The cube is null or it has the wrong numbers");
                return false;
            }
            if (side.number + side.oposedSide.number != 7)
            {
                //Debug.Log("Wrong sum, the sum of this pair is " + (side.number + side.oposedSide.number));
                return false;
            }

            for (int i = 0; i < cubeSidesNumbers.Count; i++)
            {
                if (i+1 != cubeSidesNumbers[i]) //if 123456 is not equal to 123456
                {
                    return false;
                }
            }
        }
        return true;

    }
}
