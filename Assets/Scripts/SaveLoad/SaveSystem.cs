using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private static BinaryFormatter formatter = new BinaryFormatter();
    private static string path = Application.persistentDataPath + "/playerData.data";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }      
    }

    public static void SavePlayerData(MyCharacterController player)
    {
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            return null;
        }
    }
}
