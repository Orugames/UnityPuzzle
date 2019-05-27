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
    public Vector2 position;

    public bool fixedNumber;


    private void Reset()
    {
        fixedNumber = false;
        number = 0;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateSide()
    {
        position = transform.position;
        oposedNumber = oposedSide.number;
        numberText.text = number.ToString();
        oposedNumber = oposedSide.number;
        UpdateMarker();
    }

    private void UpdateMarker()
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
        if (Input.GetMouseButtonDown(0))
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
                    }
                    oposedNumber = oposedSide.number;
                }
            }
        }

    }
}
