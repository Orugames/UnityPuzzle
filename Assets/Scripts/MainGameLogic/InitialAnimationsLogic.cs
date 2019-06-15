using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InitialAnimationsLogic : MonoBehaviour
{
    public List<GameObject> cubesGO = new List<GameObject>();
    public List<Cube> cubes = new List<Cube>();
    public List<CubeSide> cubeSides = new List<CubeSide>();


    public bool backgroundFinishedDrawing;
    public bool positionsCubeAnimFinished;
    public bool markersColorDrawnFinished;
    public bool animationsFinished;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
