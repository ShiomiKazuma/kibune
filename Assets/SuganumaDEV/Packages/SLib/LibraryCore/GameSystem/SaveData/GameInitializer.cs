using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Systems;
// �쐬 ����
public class GameInitializer : SingletonBaseClass<GameInitializer>
{
    [SerializeField]
    Transform _playerTransform;

    PlayerSaveDataSerializer _dataSerializer;

    protected override void ToDoAtAwakeSingleton()
    {
       _dataSerializer = GameObject.FindObjectOfType<PlayerSaveDataSerializer>();
    }

    public void InitializePlayer()      // �Q�[���V�[���ǂݍ��݌�ɂ����ǂݍ���
    {
        SaveDataTemplate saveDataTemplate = _dataSerializer.ReadSaveData();
        _playerTransform.position = saveDataTemplate._lastStandingPosition;
        _playerTransform.rotation = saveDataTemplate._lastStandingRotation;
        print($"{saveDataTemplate._lastStandingPosition.ToString()} : {saveDataTemplate._lastStandingRotation.ToString()}");
    }
}
