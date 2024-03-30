
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Storage
{
    public static Action<GameStateData> Loaded;

    private static string _filePath = Application.persistentDataPath + "/saves/game.gsv";

    private static BinaryFormatter _binaryFormatter = new BinaryFormatter();

    public static void Save(GameStateData stateData) 
    {
        
        FileStream stream = new FileStream(_filePath, FileMode.Create);

        _binaryFormatter.Serialize(stream, stateData);
        stream.Close();
    }

    public static GameStateData Load() 
    {
        if (File.Exists(_filePath) == false) 
            return null;

        FileStream stream = new FileStream( _filePath, FileMode.Open);
        GameStateData stateData = (GameStateData)_binaryFormatter.Deserialize(stream);
        stream.Close();

        Loaded.Invoke(stateData);

        return stateData;
    }
}
