using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CubeSide : MonoBehaviour
{
    public int number = 0;
    public int oposedNumber = 0;
    public List<Cube> cubeParents = new List<Cube>();
    public GameObject oposedSideGO;
    public CubeSide oposedSide;
    public GameObject MarkerGO;
    public Renderer MarkerGORenderer;
    public TextMeshPro numberText;
    public TextMeshPro posText;
    public Vector2 position;
    public Color cubeSideColor;
    public bool fixedNumber;
    public bool modifyValues;


    private void Reset()
    {
        fixedNumber = false;
        number = 0;
    }
    void Start()
    {
        cubeParents.Add(transform.parent.GetComponent<Cube>());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateSide()
    {
        //GetComponent<Renderer>().material.color = cubeSideColor;
        position = transform.position;
        oposedNumber = oposedSide.number;
        numberText.text = number.ToString();
        oposedNumber = oposedSide.number;
        UpdateMarker();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        posText.text = x.ToString() + "," + y.ToString();
        posText.name = x.ToString() + "," + y.ToString();
        name = "Cubeside " + posText.text;
        if (number > 6 || number <= 0)
        {
            number = 0;
            numberText.text = "";
            numberText.text = posText.text;
        }
    }

    void UpdateMarker()
    {
        foreach (Cube parentCube in cubeParents)
        {
            if (!parentCube.cubeCompleted || cubeParents == null)
            {
                MarkerGORenderer.material.color = Color.red;
            }
            else
            {
                MarkerGORenderer.material.color = Color.green;

            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && modifyValues)
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
