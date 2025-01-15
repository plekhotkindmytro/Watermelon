using UnityEngine;

[System.Serializable]
public class FruitData
{
    public int level; // Name or unique identifier of the prefab
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public string additionalData; // Any custom data (in JSON or another format)
}