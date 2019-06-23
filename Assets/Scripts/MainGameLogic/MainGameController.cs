using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainGameController : MonoBehaviour
{
    public GameObject cubeContainer;
    public List<Cube> cubes = new List<Cube>();
    public List<GameObject> cubesGO = new List<GameObject>();
    public List<CubeSide> cubeSides = new List<CubeSide>();
    public List<GameObject> cubeSidesGO = new List<GameObject>();

    public bool finishedAllAnimations;
    public bool levelCompleted;

    void Start()
    {
        cubeContainer = GameObject.Find("CubeContainer");
        cubes = SaveAndLoad.instance.cubes;
        cubesGO = SaveAndLoad.instance.cubesGO;

        foreach (GameObject cubeGO in cubesGO) //init the cubes at scale 0 and grey color
        {
            foreach (Transform childSide in cubeGO.transform)
            {
                Color originalColor = new Color(212f / 255f, 212f / 255f, 212f / 255f);

                childSide.transform.localScale = Vector3.zero;
                CubeSide side = childSide.GetComponent<CubeSide>();
                side.markerTop.GetComponent<SpriteRenderer>().color = originalColor;
                side.markerLeft.GetComponent<SpriteRenderer>().color = originalColor;
                side.markerRight.GetComponent<SpriteRenderer>().color = originalColor;
                side.markerDown.GetComponent<SpriteRenderer>().color = originalColor;

                if (!side.fixedNumber)
                {
                    side.modifyValues = true;
                    side.number = 0;
                    side.numberText.text = "";
                }
            }

        }
        SaveAndLoad.instance.CombineSides();
        UpdateLevelLists();
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedAllAnimations) OnMouseDown();
        levelCompleted = CheckCompletion();
        if (levelCompleted)
        {
            //Completed levellogic
        }
    }
    private void OnMouseDown()
    {
        UpdateLevelLists();
    }
    public void UpdateLevelLists()
    {    
        foreach (Cube cube in cubes)
        {
            cube.UpdateCube();
            cube.CheckCompletion();

           
        }
    }
    public bool CheckCompletion()
    {
        foreach (Cube cube in cubes)
        {
            if (!cube.cubeCompleted) return false;
        }
        return true;

    }
}
