﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem 
{

    public static void SaveGame()
    {

    }

    public static void LoadGame()
    {

    }

    public static void SavePlayer(PlayerStats playerStats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.p2teamdata";
        Debug.Log("Save Player in : "+path);
        FileStream stream = new FileStream(path , FileMode.Create);

        PlayerData data = new PlayerData(playerStats);

        formatter.Serialize(stream,data);

        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerdata.p2teamdata";
        Debug.Log("Load Player in : "+path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save File playerdata not found in " + path);
            return null;
        }
    }

    public static void SaveGameData(List<TeleInformation> listTeleportAllScene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.p2teamdata";
        Debug.Log("Save Gamedata in : " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(listTeleportAllScene);

        formatter.Serialize(stream, data);

        stream.Close();

        
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gamedata.p2teamdata";
        Debug.Log("Load GameData in : " + path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save File gamedata.p2teamdata not found in " + path
                + "But dont mind because we will create one");
            return null;
        }
        return null;
    }
    /*
    public static bool SaveQuest()
    {
        return false;
    }

    public static QuestData LoadQuest()
    {

        string path = Application.persistentDataPath + "/questdata.p2teamdata";
        Debug.Log("Load Quest in : " + path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            QuestData data = formatter.Deserialize(stream) as QuestData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save File questdata not found in " + path
                +"But dont mind because we will create one");
            return null;
        }
       
    }
    */

    public static OptionData LoadOptionData()
    {

        return null;
    }
    public static bool SaveOptionData()
    {
        return false;
    }

    

}

/*
        C:/Users/pham/AppData/LocalLow/P2TeamStudio/DarkUnity
 *   
    Windows Store Apps: Application.persistentDataPath points to : 
        %userprofile%\AppData\Local\Packages\<productname>\LocalState.

    iOS: Application.persistentDataPath points to : 
        /var/mobile/Containers/Data/Application/<guid>/Documents.

    Android: Application.persistentDataPath points to :
        /storage/emulated/0/Android/data/<packagename>/files 
        
        + on most devices (some older phones might point to location on SD card if present), 
        the path is resolved using :
            android.content.Context.getExternalFilesDir.
 */