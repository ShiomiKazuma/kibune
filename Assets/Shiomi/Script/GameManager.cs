using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField, Header("�Q�[���I�[�o�[���")] string _gameOverName;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GameOver()
    {
        //�Q�[���I�[�o�[��ʂɑJ��
        SceneManager.LoadScene(_gameOverName);
    }
}
