using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinedSide : CubeSide
{
    public CubeSide similarSide;
    /*public GameObject oposedSideGO;
    public CubeSide oposedSide;
    public CombinedSide MarkerGO;
    public Renderer MarkerGORenderer;
    public TextMeshPro numberText;
    public TextMeshPro posText;
    public Vector2 position;
    public Color cubeSideColor;
    public bool fixedNumber;
    public bool modifyValues;*/


    public CombinedSide()
    {

    }

    public void Init(Vector2 pos,int num, CombinedSide simSide , CubeSide i_oposedSide, Cube parent, Color parent1C, Color parent2C)
    {
        alreadyPositioned = true;
        position = pos;
        number = num;
        similarSide = simSide;
        oposedSide = i_oposedSide;
        cubeParent = parent;
        cubeSideOwnColor = (parent1C + parent2C)/ 2f;
        //transform.SetParent(parent.transform);
        UpdateSide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    new void UpdateSide()
    {
        base.UpdateSide();
        name = "Combined Cubeside " + posText.text + cubeParent.transform.position.x;

    }
}
