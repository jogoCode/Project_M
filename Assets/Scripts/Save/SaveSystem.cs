using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]

    [SerializeField] Transform _playerTransforms;
    [SerializeField] WeaponManager _weaponManager;
    Weapon _playerWeapon;
    Items _playerItem;
    private int _lvl;
    private float _xp;

    [SerializeField] NewLifeBar _lifebar;
    [SerializeField] ExpBar _expbar;
    [SerializeField] PlayerController _playerController;
    int _playerControllerHp;
    int _buffAtk;


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
        Items playerItem = _weaponManager.GetItemData();

        int Lvl = _expbar._parentExpSys.GetLevel();
        float Exp = _expbar._parentExpSys.GetExp();
        int playerController = _playerController.GetHp();
        int buffAtk = _playerController.GetBuffDamage();
        //int buffjump = _playerController.getjumpforce();
        SavedData savedData = new()
        {
            // sauvegarde de la map 
            // sauvegarde des bonus


            _playerPositions = _playerTransforms.position,
            _playerRotations = _playerTransforms.rotation,
            _playerAtkBuffs = buffAtk,
            //_playerjumpbuffs = ,
            
            _weaponInhand = playerWeapon,
            _itemInhand = playerItem,
            _lvlsaved = Lvl,
            _currentHealth = playerController,
            _currentExp = Exp,
            _playerhp = playerController,
            _maxHealth =(int)_lifebar._maxValueHolder,
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

        _playerTransforms.SetPositionAndRotation(savedData._playerPositions, savedData._playerRotations);

        _playerWeapon = savedData._weaponInhand;
        _playerItem = savedData._itemInhand;
        _buffAtk = savedData._playerAtkBuffs;
        _weaponManager.m_firstEquip = true;

        if (!_playerItem)
        {
            _weaponManager.LoadItem(_playerItem);
        }
        else if (!_playerWeapon)
        {
            _weaponManager.LoadWeapon(_playerWeapon);
        }
        
        _lvl = savedData._lvlsaved;
        _xp = savedData._currentExp;
        _lifebar._valueHolder = savedData._currentHealth;
        _lifebar._maxValueHolder = savedData._maxHealth;
        _playerControllerHp = savedData._playerhp;


        
        _playerController.SaveHp(_playerControllerHp);
        _playerController.SetDmgBuff(_buffAtk);

        //_lifebar.UpdatesValues(_lifebar._hpHolder, _lifebar._maxHpHolder);
        Debug.Log("chargement effectuée");
    }
}

public class SavedData
{
    public Vector3 _playerPositions;
    public Quaternion _playerRotations;
    public Weapon _weaponInhand;
    public Items _itemInhand;

    public int _lvlsaved;
    public float _currentExp;

    
    public float _currentHealth;
    public int _maxHealth;

    public float _currentShield;
    public int _playerhp;

    public int _playerAtkBuffs;
    public float _playerJumpBuffs;

}