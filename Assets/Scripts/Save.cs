using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

//The obejcts Inforamtion which need to be save
[System.Serializable]
public class Save
{
    public int lastSavedLevel;
    public int highestLevel;

    //Here can add customize typem, define a class named Robot, has field with its position, rotations and etc.
    //e.g public Robot[] robots;

}
