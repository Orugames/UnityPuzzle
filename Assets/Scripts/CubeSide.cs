using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeSide : MonoBehaviour
{
    public int number = 0;
    public List<Cube> cubeParents = new List<Cube>();
    public GameObject oposedSideGO;
    public CubeSide oposedSide;
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
                    if (number > 6) number = 0;
                    numberText.text = number.ToString();
                }
            }
        }

    }
}
