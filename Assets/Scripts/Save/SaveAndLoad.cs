using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public Inventory _inventory = new Inventory();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveToJson();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadFromJson();
        }
    }
    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(_inventory);
        string filePath = Application.persistentDataPath + "/inventoryData";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("Sauvegarde effectuée");
    }
    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/inventoryData";
        string inventoryData = System.IO.File.ReadAllText(filePath);

        _inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("chargement effectuée");

    }
}


[System.Serializable]
public class Inventory
{
    public Vector3 _playerPos;
    public Weapon _playerWeapon;
}

