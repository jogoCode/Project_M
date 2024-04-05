using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]

    [SerializeField] Transform _playerTransforms;
    [SerializeField] WeaponManager _weaponManager;
    [SerializeField] DayCycleManager _dayCycleManager;
    Weapon _playerWeapon;
    Items _playerItem;
    private int _lvl;
    private float _xp;

    [SerializeField] NewLifeBar _lifebar;
    [SerializeField] ExpBar _expBar;
    [SerializeField] PlayerController _playerController;
    int _playerControllerHp;
    int _playerControllerMaxHp;
    int _buffAtk;
    float _buffJump;
    float _maxXp;
    float _timeSun;


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

        int Lvl = _expBar._parentExpSys.GetLevel();
        float Exp = _expBar._parentExpSys.GetExp();
        int playerController = _playerController.GetHp();
        int buffAtk = _playerController.GetBuffDamage();
        float buffjump = _playerController.GetJumpForce();
        int maxHp = _playerController.GetMaxhp();
        float maxXp = _expBar._parentExpSys.GetMaxExp();
        float time = _dayCycleManager.IngameTime;

        SavedData savedData = new()
        {
            // sauvegarde de la map 
            // sauvegarde des bonus


            _playerPositions = _playerTransforms.position,
            _playerRotations = _playerTransforms.rotation,
            _playerAtkBuffs = buffAtk,
            _playerJumpBuffs = buffjump,
            _weaponInhand = playerWeapon,
            _itemInhand = playerItem,
            _lvlsaved = Lvl,
            _currentHealth = playerController,
            _currentExp = Exp,
            _playerhp = playerController,
            _maxHealth = maxHp,
            _maxExp = maxXp,
            _time = time,
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
        _buffJump = savedData._playerJumpBuffs;
        _weaponManager.m_firstEquip = true;
        _timeSun = savedData._time;

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
        //_lifebar._valueHolder = savedData._currentHealth;
        _playerControllerMaxHp = savedData._maxHealth;
        _playerControllerHp = savedData._playerhp;
        _dayCycleManager.LoadTime(_timeSun);



        _playerController.LoadMaxHp(savedData._maxHealth);
        _playerController.SaveHp(_playerControllerHp);
        _playerController.LoadDmgBuff(_buffAtk);
        _playerController.LoadJumpSpeed(_buffJump);
        _playerController.GetLvlSystem().LoadLvl(savedData._lvlsaved);
        _playerController.GetLvlSystem().LoadXp(savedData._currentExp);
        _expBar.UpdatesValues(savedData._currentExp, savedData._maxExp);

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
    public float _maxExp;


    public float _currentHealth;
    public int _maxHealth;

    public float _currentShield;
    public int _playerhp;

    public int _playerAtkBuffs;
    public float _playerJumpBuffs;

    public float _time;

    


}