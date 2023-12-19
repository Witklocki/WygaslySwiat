using NPCModel;
using PlayerDataModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PlayerDataModel
{
    public class PlayerDataObject
    {
        public int id;
        public float healthPoint;
        public float maxHealth;
        public float attack;
        public float moveSpeed;
        public float defense;
        public float attackSpeed;
    }
}

[CreateAssetMenu(fileName = "New Player Object", menuName = "Inventory System/Player/Player")]

public class PlayerObject :ScriptableObject
{
    PlayerDataObject _playerData;
    private string fileName = "Assets/Resources/Data/Player.json";

    public float healthPoint   //getter setter for healthPoints
    {
        get => _playerData.healthPoint;
        set => _playerData.healthPoint = value;
    }

    public float maxHealth   //getter setter for healthPoints
    {
        get => _playerData.maxHealth;
        set => _playerData.maxHealth = value;
    }

    public float attack   //getter setter for healthPoints
    {
        get => _playerData.attack;
        set => _playerData.attack = value;
    }

    public float moveSpeed   //getter setter for healthPoints
    {
        get => _playerData.moveSpeed;
        set => _playerData.moveSpeed = value;
    }

    public float defense
    {
        get => _playerData.defense;
        set => _playerData.defense = value;
    }


    public void Reset()
    {
        _playerData.healthPoint = _playerData.maxHealth;
    }

    public void upgradeStatistic(string element, float amount)
    {
        switch (element) {
            case "maxHealth":
                _playerData.maxHealth += amount; 
                break;
        }
    }

    public void IncreasMaxHealth(float number)
    {
        _playerData.maxHealth += number;
        Reset();
    }

    public void IncreasAttack(float number)
    {
        _playerData.attack += number;
    }

    public void IncreasDefense(float number)
    {
        _playerData.defense += number;
    }
    public void readJson()
    {
        string jsonString = File.ReadAllText(fileName);
        _playerData = JsonUtility.FromJson<PlayerDataObject>(jsonString);
    }

    public void readJson(string path)
    {
        string jsonString = File.ReadAllText(path);
        _playerData = JsonUtility.FromJson<PlayerDataObject>(jsonString);
    }

    public void writeJson()
    {
        string jsonString = JsonUtility.ToJson(_playerData, true);
        File.WriteAllText(fileName, jsonString);
    }

}