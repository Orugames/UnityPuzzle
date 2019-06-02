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
        //transform.GetChild(0).GetComponent<Renderer>().material.color = cubeSideColor;
        MarkerGORenderer.material.color = cubeSideColor;
        MarkerGORenderer = MarkerGO.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateSide()
    {
        cubeParent = transform.parent.GetComponent<Cube>();
        cubeSideColor = cubeParent.color;
        //transform.GetChild(0).GetComponent<Renderer>().material.color = cubeSideColor;

        MarkerGORenderer.material.color = cubeSideColor;
        numberText.color = Color.white;
        posText.color = Color.black;
        position = transform.position;
        MarkerGORenderer = MarkerGO.GetComponent<Renderer>();

        if (oposedSide != null) {
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
        //If the number is not between 1 and 6
        if (number > 6 || number <= 0)
        {
            number = 0;
            numberText.text = "";
            //numberText.text = posText.text;
        }
        //Logic if the number is fixed
        if (fixedNumber)
        {
            //we keep the value but still hide the rest
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white * 0.5f;
            numberText.color = Color.black;

        }
        //Logic if the numbers must be hidden
        numberText.renderer.enabled = true;
        numberText.alpha = 255;

        if (hideNumbers && !fixedNumber)
        {
            numberText.renderer.enabled = false;
            numberText.alpha = 0;
        }
        
        //Logic if the side is combination of two cubes
        if (combinedCube)
        {
            cubeSideColor = (cubeParents[0].color + cubeParents[1].color) / 2;
            //transform.GetChild(0).GetComponent<Renderer>().material.color = cubeSideColor;
            MarkerGORenderer.material.color = cubeSideColor;
            similarCubeSide.number = number;


        }
    }

    void UpdateMarker()
    {
        if (cubeParent != null)
            if (!cubeParent.cubeCompleted)
            {
                //MarkerGORenderer.material.color = Color.black + Color.white * 0.8f;
                //numberText.color = Color.black;
                //posText.color = Color.black;
}
            else
            {
                //MarkerGORenderer.material.color = (Color.white + Color.green) * 0.7f;
                posText.color = Color.green;
                //numberText.color = Color.green/2;
                //posText.color = Color.green/2;
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
                    numberText.text = number.ToString();
                    if (number > 6)
                    {
                        number = 0;
                        numberText.text = "";
                        numberText.text = posText.text;
                    }
                    oposedNumber = oposedSide.number;
                }
            }
        }

    }
    public void ToggleModifyValues()
    {
        modifyValues = !modifyValues;
    }
}
