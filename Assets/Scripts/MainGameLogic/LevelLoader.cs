using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadLevel(int levelNumber)
    {

        Debug.Log("Load level " + levelNumber);
        SceneManager.LoadSceneAsync(1);

        SaveAndLoad.instance.levelSelected = levelNumber;

        SaveAndLoad.instance.LoadData(levelNumber);
        //SaveAndLoad.instance.singletonEnabled = false;
        this.enabled = false;

    }
}
