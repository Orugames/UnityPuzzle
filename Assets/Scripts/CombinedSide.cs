using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinedSide : CubeSide
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


    public CombinedSide()
    {

    }

    public CombinedSide(Vector2 pos,int num, CombinedSide similarSide , CubeSide oposedSide)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
