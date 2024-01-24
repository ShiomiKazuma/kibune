using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBaseClass<GameManager> 
{
    public static GameManager instance;
    [SerializeField, Header("ゲームオーバー画面")] string _gameOverName;

    public void GameOver()
    {
        //ゲームオーバー画面に遷移
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameOverName);
    }

    protected override void ToDoAtAwakeSingleton() { }
}
