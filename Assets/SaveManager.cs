using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Spawner spawner;
   // private string saveFilePath => Path.Combine(Application.persistentDataPath, "saveData.json");

    private List<Transform> objectsToSave;
    private List<FruitData> fruitDatas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SaveData()
    {
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            Transform child = spawner.transform.GetChild(i);
            objectsToSave.Add(child);
            FruitData fruitData = new FruitData();
            Fruit fruit = child.GetComponent<Fruit>();
            fruitData.level = fruit.fruitLevel;
            fruitData.position = child.position;
            fruitData.rotation = child.rotation;
            fruitData.scale = child.localScale;
            fruitDatas.Add(fruitData);
        }

        string json = JsonUtility.ToJson(new { data = fruitDatas }, true);
        string saveFilePath = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetInt(ThemeManager.THEME_KEY) + "saveData.json");
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Scene saved to {saveFilePath}");
    }


    public void LoadData()
    {
        int themeIndex = PlayerPrefs.GetInt(ThemeManager.THEME_KEY);
        Theme theme = ThemeManager.Instance.themes[themeIndex];
        GameObject[] fruitPrefabs = theme.fruits;
        string saveFilePath = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetInt(ThemeManager.THEME_KEY) + "saveData.json");

        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        var dataWrapper = JsonUtility.FromJson<DataWrapper>(json);

        foreach (var data in dataWrapper.data)
        {
            GameObject fruitPrefab = fruitPrefabs[data.level - 1];
            var obj = Instantiate(fruitPrefab, data.position, data.rotation);
            obj.transform.parent = spawner.transform;
            obj.transform.localScale = data.scale;
        }
    }

    public void DeleteData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, PlayerPrefs.GetInt(ThemeManager.THEME_KEY) + "saveData.json");

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnSceneUnload()
    {
        SaveData();
    }

    void OnDestroy()
    {

        
            SaveData(); 

    }




    [System.Serializable]
    private class DataWrapper
    {
        public List<FruitData> data;
    }

}
