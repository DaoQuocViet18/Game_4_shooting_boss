using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string namePlayer;
    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        loadBestScore();
    }

    [System.Serializable]
    class saveData
    {
        public string name;
        public int bestScore;
    }    

    public void saveBestScore(string nPlayer, int bScore)
    {
        saveData data = new saveData();
        data.bestScore = bScore;
        data.name = nPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefileShooting.json", json);
    }    

    public void loadBestScore()
    {
        string path = Application.persistentDataPath + "/savefileShooting.json";
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            saveData data = JsonUtility.FromJson<saveData>(json);

            bestScore = data.bestScore;
            namePlayer = data.name;
        }
    }    
}
