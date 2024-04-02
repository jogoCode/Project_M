using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Transform _playerTransforms;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            LoadData();
        }
    }

     void SaveData()
    {
        SavedData savedData = new()
        {
            playerPositions = _playerTransforms.position,
            playerRotations = _playerTransforms.rotation,
        };

        string jsonData = JsonUtility.ToJson(savedData);
        string filePath = Application.persistentDataPath + "/SavedData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, jsonData);
        Debug.Log("Save terminé");
    }
    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/SavedData.json";
        string jsonData = System.IO.File.ReadAllText(filePath);

        SavedData savedData = JsonUtility.FromJson<SavedData>(jsonData);
        _playerTransforms.position = savedData.playerPositions;
        _playerTransforms.rotation = savedData.playerRotations;
        Debug.Log("chargement effectuée");
    }
}

public class SavedData
{
    public Vector3 playerPositions;
    public Quaternion playerRotations;
}