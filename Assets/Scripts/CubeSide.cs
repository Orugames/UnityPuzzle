using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CubeSide : MonoBehaviour
{
    public bool alreadyPositioned;
    public int number = 0;
    public int oposedNumber = 0;
    public Cube cubeParent;
    public List<Cube> cubeParents = new List<Cube>();
    public GameObject oposedSideGO;
    public List<CubeSide> similarCubeSides = new List<CubeSide>();
    public CubeSide oposedSide;
    public TextMeshPro numberText;
    public TextMeshPro posText;
    public Vector2 position;
    public Color cubeSideOwnColor;
    public GameObject markerTop;
    public GameObject markerLeft;
    public GameObject markerRight;
    public GameObject markerDown;
    public GameObject correctCubeDot;
    public bool fixedNumber;
    public bool modifyValues;
    public bool combinedCube;
    public bool hideNumbers;
    public Material tileMat;
    Color originalColor = new Color(212f/255f,212f/255f,212f/255f);

    //public enum markers { top, left, right, down }
    //public markers chosenMarker = markers.top;

    private void Reset()
    {
        fixedNumber = false;
        number = 0;
    }
    void Start()
    {
        cubeParent = transform.parent.GetComponent<Cube>();
        UpdateSide();
        markerTop.GetComponent<SpriteRenderer>().color = originalColor;
    }

 

    public void UpdateSide()
    {
        cubeParent = transform.parent.GetComponent<Cube>();
        cubeSideOwnColor = cubeParent.color;
        numberText.color = Color.white;
        posText.color = Color.black;
        position = transform.position;

        if (oposedSide != null)
        {
            oposedNumber = oposedSide.number;
            oposedSideGO = oposedSide.gameObject;
        }

        numberText.text = number.ToString();
        UpdateMarker();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        posText.text = x.ToString() + "," + y.ToString();
        posText.name = x.ToString() + "," + y.ToString();
        name = "Cubeside " + posText.text;
        LimitNumberCount();

        if (!combinedCube) MarkerColorsLogic(cubeParent.chosenMarker);
        //Logic if the number is fixed
        FixedSideLogic();
        //Logic if the numbers must be hidden
        HideNumbersLogic();

        //Logic if the side is combination of two cubes
        CombinedSideLogic();
    }

    public  void MarkerColorsLogic(markers markerSent)
    {
        markerTop.GetComponent<SpriteRenderer>().color = originalColor;
        markerLeft.GetComponent<SpriteRenderer>().color = originalColor;
        markerRight.GetComponent<SpriteRenderer>().color = originalColor;
        markerDown.GetComponent<SpriteRenderer>().color = originalColor;
        switch (markerSent)
        {
            case markers.top:
                markerTop.GetComponent<SpriteRenderer>().color = cubeSideOwnColor;

                break;
            case markers.left:
                markerLeft.GetComponent<SpriteRenderer>().color = cubeSideOwnColor;

                break;
            case markers.right:
                markerRight.GetComponent<SpriteRenderer>().color = cubeSideOwnColor;

                break;
            case markers.down:
                markerDown.GetComponent<SpriteRenderer>().color = cubeSideOwnColor;

                break;

        }

    }
    public void MarkerColorsLogicCombined(markers markerSent, Color cubeParentColor)
    {
 
        switch (markerSent)
        {
            case markers.top:
                markerTop.GetComponent<SpriteRenderer>().color = cubeParentColor;
                break;
            case markers.left:
                markerLeft.GetComponent<SpriteRenderer>().color = cubeParentColor;
                break;
            case markers.right:
                markerRight.GetComponent<SpriteRenderer>().color = cubeParentColor;
                break;
            case markers.down:
                markerDown.GetComponent<SpriteRenderer>().color = cubeParentColor;
                break;

        }
    }

    private void CombinedSideLogic()
    {
        if (combinedCube)
        {

            foreach(Cube cube in cubeParents)
            {
                MarkerColorsLogicCombined(cube.chosenMarker,cube.color);
                
            }
            foreach (CubeSide side in similarCubeSides)
            {
                side.number = number;
            }

        }
    }

    private void HideNumbersLogic()
    {
        numberText.renderer.enabled = true;
        numberText.alpha = 255;

        if (hideNumbers && !fixedNumber)
        {
            numberText.renderer.enabled = false;
            numberText.alpha = 0;
        }
    }

    private void FixedSideLogic()
    {
        if (fixedNumber)
        {
            //we keep the value but still hide the rest
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white * 0.5f;
            numberText.color = Color.black;

        }
    }

    private void LimitNumberCount()
    {
        //If the number is not between 1 and 6
        if (number > 6 || number <= 0)
        {
            number = 0;
            numberText.text = "";
            //numberText.text = posText.text;
        }
    }

    void UpdateMarker()
    {
        if (cubeParent != null)
            if (!cubeParent.cubeCompleted)
            {
                correctCubeDot.SetActive(false);
            }
            else
            {
                correctCubeDot.SetActive(true);
            }
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && modifyValues && !fixedNumber)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    number += 1;
                    UpdateSide();
                    //numberText.text = number.ToString();
                    /*if (number > 6)
                    {
                        number = 0;
                        numberText.text = "";
                        numberText.text = posText.text;
                    }
                    oposedNumber = oposedSide.number;*/
                }
            }
        }

    }
    public void ToggleModifyValues()
    {
        modifyValues = !modifyValues;
    }
}
