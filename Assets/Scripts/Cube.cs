using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum markers { top, left, right, down }

public class Cube : MonoBehaviour
{
    public Color color;
    public Vector2 position;
    public Quaternion rotation;
    public int numbersRotations;
    public bool cubeCompleted;
    public GameObject prefab;
    public int prefabNum;
    public List<CubeSide> cubeSides = new List<CubeSide>();
    public List<int> cubeSidesNumbers = new List<int>();
    public List<Color> presetColors = new List<Color> { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow };
    public markers chosenMarker = markers.top;

    public CubeSide side1Solver;
    public CubeSide side2Solver;
    public CubeSide side3Solver;

    public void Start()
    {
        InitColor();
        RandomChosenMarker();

        foreach (Transform child in transform)
        {
            cubeSides.Add(child.GetComponent<CubeSide>());


            child.GetComponent<CubeSide>().cubeSideOwnColor = color;
            //child.GetComponent<CubeSide>().MarkerColorsLogic(chosenMarker);
        }
        name = "Cube " + transform.position.x.ToString() + " , " + transform.position.y.ToString();

    }

    private void InitColor()
    {
        if (color == transform.GetChild(0).GetComponent<CubeSide>().cubeSideOwnColor) //here we init the color
        {
            color = new Color();

            color = presetColors[UnityEngine.Random.Range(0, presetColors.Count)] *0.95f;
        }
    }

  

    public void UpdateCube()
    {
        position = transform.position;
        if (numbersRotations > 3) numbersRotations = 0;
        transform.eulerAngles = new Vector3(0,0, numbersRotations * 90);
        cubeSidesNumbers.Clear();
        cubeSides.Clear();
        foreach (Transform child in transform)
        {
            cubeSides.Add(child.GetComponent<CubeSide>());            

            child.GetComponent<CubeSide>().cubeSideOwnColor = color;
        }
        foreach (CubeSide cubeSide in cubeSides)
        {
            cubeSide.UpdateSide();
            //cubeSide.MarkerColorsLogic(chosenMarker);

            cubeSidesNumbers.Add(cubeSide.number);

        }      
        cubeSidesNumbers.Sort();

        /*Debug.Log(String.Join("",
             new List<int>(cubeSidesNumbers)
             .ConvertAll(i => i.ToString())
             .ToArray()));*/

        cubeCompleted = CheckCompletion();

    }

    private void RandomChosenMarker()
    {
        int randMarkerInt = UnityEngine.Random.Range(1, 5);
        switch (randMarkerInt)
        {
            case 1:
                chosenMarker = markers.top;
                break;
            case 2:
                chosenMarker = markers.left;
                break;
            case 3:
                chosenMarker = markers.right;
                break;
            case 4:
                chosenMarker = markers.down;
                break;

        }
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
