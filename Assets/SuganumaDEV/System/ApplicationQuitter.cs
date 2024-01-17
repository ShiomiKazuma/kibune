using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuitter : SingletonBaseClass<ApplicationQuitter>
{
    [SerializeField]
    Transform _playerTransform;

    PlayerSaveDataCreator _playerSaveDataCreator;

    protected override void ToDoAtAwakeSingleton()
    {
        _playerSaveDataCreator = GameObject.FindFirstObjectByType<PlayerSaveDataCreator>();
    }

    public void QuitApplication()
    {
        _playerSaveDataCreator.SavePlayerData(_playerTransform, SceneManager.GetActiveScene().name);
    }

}
