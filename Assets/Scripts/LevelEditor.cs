using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : MonoBehaviour
{
    float gridSize = 1;

    public  GameObject prefab;
    public bool modifyValues;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnMouseDown()
    {
      
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 point2 = hit.point;
                GameObject newPrefab = Instantiate(prefab);

                Vector3 rounded;
                rounded.x = Mathf.RoundToInt((hit.point.x * gridSize) / gridSize);
                rounded.y = Mathf.RoundToInt((hit.point.y * gridSize) / gridSize);
                rounded.z = 0;
                newPrefab.transform.position = rounded;
                newPrefab.GetComponent<GridCubeLogic>().Initialize();
            }
        

    }

    

}
