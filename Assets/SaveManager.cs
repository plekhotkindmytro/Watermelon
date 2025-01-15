using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor.U2D.Aseprite;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Spawner spawner;
   // private string saveFilePath => Path.Combine(Application.persistentDataPath, "saveData.json");

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
        print("count: " + spawner.transform.childCount);
        fruitDatas = new List<FruitData>();
        for (int i = 0; i < spawner.transform.childCount; i++)
        {
            Transform child = spawner.transform.GetChild(i);
            
            FruitData fruitData = new FruitData();
            Fruit fruit = child.GetComponent<Fruit>();
            if(fruit.GetComponent<Collider2D>().enabled) {
                fruitData.level = fruit.fruitLevel;
                fruitData.position = child.position;
                fruitData.rotation = child.rotation;
                fruitData.scale = child.localScale;
                fruitDatas.Add(fruitData);
            }
           
        }


        DataWrapper<FruitData> wrapper = new DataWrapper<FruitData>(fruitDatas);
        string json = JsonUtility.ToJson(wrapper);

        print(json);
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
        DataWrapper<FruitData> wrapper = JsonUtility.FromJson<DataWrapper<FruitData>>(json);
        if (wrapper != null && wrapper.items != null)
        {
            foreach (var data in wrapper.items)
            {
                print(data);
                GameObject fruitPrefab = fruitPrefabs[data.level - 1];
                var obj = Instantiate(fruitPrefab, data.position, data.rotation);
                obj.transform.parent = spawner.transform;
                obj.transform.localScale = data.scale;
                obj.GetComponent<Fruit>().ActivateMe();
            }
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

    // private void OnSceneUnload()
    // {
    //     SaveData();
    // }

    // void OnDestroy()
    // {

        
    //         SaveData(); 

    // }




   [System.Serializable]
public class DataWrapper<T>  {
    public List<T> items;

    public DataWrapper(List<T> items)
    {
        this.items = items;
    }
}
}
