using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]

    [SerializeField] Transform _playerTransforms;
    [SerializeField] WeaponManager _weaponManager;
    Weapon playerWeapon;

    Items playerItem ;
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
        Weapon playerWeapon = _weaponManager.GetWeaponData();

        GameObject visualParent = _weaponManager.GetVisualData();

        Items playerItem = _weaponManager.GetItemData();

        SavedData savedData = new()
        {
            _playerPositions = _playerTransforms.position,
            _playerRotations = _playerTransforms.rotation,
            _weaponInhand = playerWeapon,
            _itemInhand = playerItem,

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

        _playerTransforms.position = savedData._playerPositions;
        _playerTransforms.rotation = savedData._playerRotations;

        playerWeapon = savedData._weaponInhand;
        playerItem = savedData._itemInhand;

        _weaponManager.EquipItem(playerItem);
        _weaponManager.EquipWeapon(playerWeapon);

        Debug.Log("chargement effectuée");
    }
}

public class SavedData
{
    public Vector3 _playerPositions;
    public Quaternion _playerRotations;
    public Weapon _weaponInhand;
    public Items _itemInhand;

    public float currentHealth;
    public float currentShield;
    public float currentExp;
}