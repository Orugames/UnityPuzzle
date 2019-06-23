using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class InitialAnimationsLogic : MonoBehaviour
{
    public List<GameObject> cubesGO = new List<GameObject>();
    public List<Cube> cubes = new List<Cube>();
    public List<CubeSide> cubeSides = new List<CubeSide>();

    public MainGameController mainGameController;
    public GameObject canvas;
    public GameObject backgroundCubePrefab;
    public List<GameObject> backgroundCubes = new List<GameObject>();

    public RectTransform levelInfoTextRT;
    public RectTransform helpButtonRT;
    public RectTransform undoMoveButtonRT;
    public RectTransform menuButtonRT;


    public bool backgroundFinishedDrawing;
    public bool positionsCubeAnimFinished;
    public bool markersColorDrawnFinished;
    public bool animationsFinished;

    void Start()
    {
        cubes = SaveAndLoad.instance.cubes;
        cubesGO = SaveAndLoad.instance.cubesGO;
        cubeSides = SaveAndLoad.instance.cubeSides;

        /*foreach (GameObject cubeGO in cubesGO) //init the cubes at scale 0 and grey color
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
            }

        }
        SaveAndLoad.instance.CombineSides();*/

        for (int i = -6; i < 7; i++)
        {
            for (int j = -3; j < 4; j++)
            {
                GameObject newCubeBg = Instantiate(backgroundCubePrefab, canvas.transform);
                newCubeBg.transform.position = new Vector2(j, i);
                newCubeBg.transform.localScale = Vector3.zero;
                backgroundCubes.Add(newCubeBg);
            }
        }
        backgroundCubes.Reverse();
        StartCoroutine(BackgroundCubesAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator BackgroundCubesAnimation()
    {
        foreach (GameObject bgCube in backgroundCubes)
        {
            bgCube.transform.DOScale(1, 0.15f);
            yield return null;

        }
        StartCoroutine(CubesAnimation());

    }
    public IEnumerator CubesAnimation()
    {
        foreach (GameObject cubeGO in cubesGO)
        {
            foreach (Transform childSide in cubeGO.transform)
            {
                childSide.transform.DOScale(1, 0.25f);
                yield return null;
            }

        }
        TrimExcessBgCubes();
        StartCoroutine(MarkersColorsAnim());

    }

    private void TrimExcessBgCubes()
    {    
        for (int i = 0; i < backgroundCubes.Count; i++)
        {
            for (int p = 0; p < cubeSides.Count; p++)
            {
                if (i != p)
                {
                    if (backgroundCubes[i].transform.position == cubeSides[p].transform.position)
                    {
                        GameObject bgToDelete = backgroundCubes[i];
                        backgroundCubes.RemoveAt(i);
                        Destroy(bgToDelete);
                    }
                }
            }
        }

    }
    public IEnumerator MarkersColorsAnim()
    {
        foreach (Cube cube in cubes)
        {
            foreach (CubeSide childSide in cube.cubeSides)
            {
                foreach (KeyValuePair<GameObject, Color> pair in childSide.markersPainted)
                {
                    pair.Key.GetComponent<SpriteRenderer>().DOColor(pair.Value, 2); //we get the key marker and apply its color value stored
                }
                yield return null;
            }

        }
        StartCoroutine(MenuButtons());
        mainGameController.finishedAllAnimations = true;
    }

    public IEnumerator MenuButtons()
    {
        levelInfoTextRT.DOAnchorPosY(-266, 0.5f);
        yield return new WaitForSeconds(0.25f);
        helpButtonRT.DOAnchorPosY(-285, 0.5f);
        yield return new WaitForSeconds(0.25f);
        undoMoveButtonRT.DOAnchorPosY(-285, 0.5f);
        yield return new WaitForSeconds(0.25f);
        menuButtonRT.DOAnchorPosY(-285, 0.5f);
        yield return new WaitForSeconds(0.25f);


    }

}
