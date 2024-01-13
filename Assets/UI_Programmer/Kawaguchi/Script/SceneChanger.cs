using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    static SceneChanger _instance;
    public static SceneChanger Instance => _instance ?? (_instance = new SceneChanger());

    [Header("フェードアウトに使うパネル")]
    [SerializeField]
    private Graphic _fadePanel;

    [SerializeField] float _LoadSpeed;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string name)
    {
        StartCoroutine(Execute(name));
    }

    IEnumerator Execute(string name)
    {
        yield return FadeOut();
        SceneManager.LoadScene(name);
        yield return FadeIn();
    }

    /// <summary>
    /// 一応外からも呼び出せるようにしておくフェードイン用のコルーチン
    /// </summary>
    IEnumerator FadeIn(float duration = 0.4f)
    {
        if (_fadePanel != null)
        {
            _fadePanel.gameObject.SetActive(true);
            _fadePanel.DOFade(1.0f, 0.0f);
            yield return _fadePanel.DOFade(0.0f, duration).WaitForCompletion();
            _fadePanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("フェードアウトに使うパネルが設定されていません");
            yield break;
        }

        Debug.Log("フェードイン完了");
    }

    /// <summary>
    /// 外から呼び出すシーン遷移用のコルーチン
    /// </summary>
    IEnumerator FadeOut(float duration = 0.4f)
    {
        // DOFadeによるフェードアウトを行う
        if (_fadePanel != null)
        {
            _fadePanel.gameObject.SetActive(true);
            yield return _fadePanel.DOFade(1.0f, duration).WaitForCompletion();
        }
        else
        {
            Debug.LogError("フェードアウトに使うパネルが設定されていません");
        }
    }

    /// <summary>
    /// ボタンから呼び出す用のシーン遷移用のメソッド
    /// </summary>
    public void ChangeScene(string name)
    {
        StartCoroutine(Execute(name));
    }
}