using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManager : MonoBehaviour
{
    public float InputSensitivity = 0.5f;
    public float MusicVolume = 0.5f;
    public float SfxVolume = 0.5f;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartNewGame()
    {
        Save save = new Save();
        save.lastSavedLevel = 2;
        save.highestLevel = 2;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/mgp2save" + ".dat");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game saved");
        SceneManager.LoadScene(2);

    }

    public void ContinueGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/mgp2save" + ".dat", FileMode.Open);
        Save save = (Save)bf.Deserialize(file);
        file.Close();
        SceneManager.LoadScene(save.lastSavedLevel); 

    }



    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
    {
        Debug.Log("Load called");
        SceneManager.LoadScene(level);
    }


    public int GetHighestLevel()
    {
        if(File.Exists(Application.persistentDataPath + "/mgp2save" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mgp2save" + ".dat", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            return save.highestLevel;

        }
        return -1;
    }

    public void SaveLevel()
    {
        
        int levelOnPlaying = SceneManager.GetActiveScene().buildIndex;
        int previousHighestLevel = GetHighestLevel(); 
        Save save = new Save();

        save.lastSavedLevel = levelOnPlaying;
        if(levelOnPlaying > previousHighestLevel)
            save.highestLevel = levelOnPlaying;
        else
            save.highestLevel = previousHighestLevel;
        

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/mgp2save" + ".dat");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game saved previous highest level " + previousHighestLevel + " current highest level " + save.highestLevel);

    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME!");
        Application.Quit();
    }

    public void setMusicVolume(float v)
    {
        MusicVolume = v;
    }
    public void setSfxVolume(float v)
    {
        SfxVolume = v;
    }
    public void setSensitivity(float s)
    {
        InputSensitivity = s;
    }
}
