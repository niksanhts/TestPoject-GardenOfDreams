
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Storage
{
    private static BinaryFormatter _binaryFormatter = new BinaryFormatter();

    public static void Save(string objName, string variableName, object data)
    {
        var filePath = BuildPath(objName, variableName);

        //if (Directory.Exists(filePath) == false)
        //    Directory.CreateDirectory(filePath);

        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
       
        _binaryFormatter.Serialize(stream, data);

        stream.Close();
    }

    public static object Load(string objName, string variableName)
    {
        var filePath = BuildPath(objName, variableName);

        if (File.Exists(filePath) == false)
                return null;

        FileStream stream = new FileStream(filePath, FileMode.Open);
        
        object data = _binaryFormatter.Deserialize(stream);

        stream.Close();

        return data;
    }

    private static string BuildPath(string objName, string variableName)
    {
        return Path.Combine(Application.persistentDataPath, objName, variableName + ".gsv");
    }
}
