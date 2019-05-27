using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Color color;
    public bool cubeCompleted;
    public List<CubeSide> cubeSides = new List<CubeSide>();

    void Start()
    {
        color = new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f));

        foreach (Transform child in transform)
        {
            cubeSides.Add(child.GetComponent<CubeSide>());



            child.GetComponent<CubeSide>().cubeSideColor = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCube();
        //cubeCompleted = CheckCompletion();
        if (cubeCompleted)
        {
            //Debug.Log("yeahhhhh");
        }
    }
    public void UpdateCube()
    {
        foreach (CubeSide cubeSide in cubeSides)
        {
            cubeSide.UpdateSide();
        }
    }


    private bool CheckCompletion()
    {
        foreach (CubeSide side in cubeSides)
        {
            if (side.number == 0 || side.number < 0 || side.number > 6 || cubeSides.Count != 6)
            {
                Debug.Log("The cube is null or it has the wrong numbers");
                return false;
            }
            if (side.number + side.oposedSide.number != 7)
            {
                Debug.Log("Wrong sum, the sum of this pair is " + (side.number + side.oposedSide.number));
                return false;
            }
        }
        return true;

    }
}
