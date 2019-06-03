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
    public CubeSide similarCubeSide;
    public CubeSide oposedSide;
    public GameObject MarkerGO;
    public Renderer MarkerGORenderer;
    public TextMeshPro numberText;
    public TextMeshPro posText;
    public Vector2 position;
    public Color cubeSideColor;
    public bool fixedNumber;
    public bool modifyValues;
    public bool combinedCube;
    public bool hideNumbers;



    private void Reset()
    {
        fixedNumber = false;
        number = 0;
    }
    void Start()
    {
        cubeParent = transform.parent.GetComponent<Cube>();
        MarkerGORenderer.material.color = cubeSideColor;
        MarkerGORenderer = MarkerGO.GetComponent<Renderer>();
        UpdateSide();
    }

    
    public void UpdateSide()
    {
        cubeParent = transform.parent.GetComponent<Cube>();
        cubeSideColor = cubeParent.color;
        MarkerGORenderer.material.color = cubeSideColor;
        numberText.color = Color.white;
        posText.color = Color.black;
        position = transform.position;
        MarkerGORenderer = MarkerGO.GetComponent<Renderer>();

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

        //Logic if the number is fixed
        FixedSideLogic();
        //Logic if the numbers must be hidden
        HideNumbersLogic();

        //Logic if the side is combination of two cubes
        CombinedSideLogic();
    }

    private void CombinedSideLogic()
    {
        if (combinedCube)
        {
            cubeSideColor = (cubeParents[0].color + cubeParents[1].color) / 2;
            //transform.GetChild(0).GetComponent<Renderer>().material.color = cubeSideColor;
            MarkerGORenderer.material.color = cubeSideColor;
            similarCubeSide.number = number;


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
}
            else
            {
                posText.color = Color.green;
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
