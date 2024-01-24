using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBaseClass<GameManager> 
{
    public static GameManager instance;
    [SerializeField, Header("�Q�[���I�[�o�[���")] string _gameOverName;

    public void GameOver()
    {
        //�Q�[���I�[�o�[��ʂɑJ��
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameOverName);
    }

    protected override void ToDoAtAwakeSingleton() { }
}
