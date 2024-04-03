using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]

    [SerializeField] Transform _playerTransforms;
    [SerializeField] WeaponManager _weaponManager;


    Weapon playerWeapon;

    GameObject visualParent ;

    Items playerItem ;
    private void Start()
    {

        Weapon playerWeapon = _weaponManager.GetWeaponData();

        GameObject visualParent = _weaponManager.GetVisualData();

        Items playerItem = _weaponManager.GetItemData();
    }
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
            _playerPositions = _playerTransforms.position,
            _playerRotations = _playerTransforms.rotation,
            _weaponInhand = playerWeapon,
            _itemInhand = playerItem,
            _visual = visualParent,

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
        visualParent = savedData._visual;
    
        Debug.Log("chargement effectuée");
    }
}

public class SavedData
{
    public Vector3 _playerPositions;
    public Quaternion _playerRotations;
    public Weapon _weaponInhand;
    public Items _itemInhand;
    public GameObject _visual;

    

   
}