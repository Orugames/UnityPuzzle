using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class GridCubeLogic : MonoBehaviour
{
    public TextMeshPro posText;

    public int x;
    public int y;
    public bool init;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (init) Initialize();
    }

    public  void Initialize()
    {
        init = false;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        posText.text = x.ToString() + "," + y.ToString();
        posText.name = x.ToString() + "," + y.ToString();
        name = "Cubeside " + posText.text;
    }
}
