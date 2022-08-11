using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //reference for the buttons
    public GameObject[] buttonLevel = new GameObject[6];
    GameManager gameManager = new GameManager();
    // Start is called before the first frame update
    void Start()
    {
        //activate the right buttons based on which level were on
        int highestLevel = gameManager.GetHighestLevel();
        for(int i = 0; i < 6; i++)
        {
            if(i < highestLevel-1) buttonLevel[i].SetActive(true);
            else buttonLevel[i].SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
