using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    static SceneManager _instance;

    [Header("�t�F�[�h�̐ݒ�")]
    [SerializeField] RawImage _fadeImage;
    [SerializeField] Text _fadeText;
    [SerializeField] float _fadeSpeed = 1.0f;


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

    void Start()
    {
        // �N�����̃^�C�g���V�[���Ńt�F�[�h�C������
        _instance.StartCoroutine(_instance.FadeIn());
    }

    /// <summary>
    /// �t�F�[�h�A�E�g��A�V�[���J�ځA�t�F�[�h�C������
    /// </summary>
    /// <param name="name">���̃V�[����</param>
    public static void SceneChange(string name)
    {
        _instance.StartCoroutine(_instance.LoadScene(name));
    }

    IEnumerator LoadScene(string name)
    {
        yield return FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        yield return FadeIn();
    }

    IEnumerator FadeIn()
    {
        while (_instance._fadeImage.color.a > 0)
        {
            Color color = _instance._fadeImage.color;
            color.a -= Time.deltaTime;
            _instance._fadeImage.color = color;
            yield return null;
        }

        _fadeImage.enabled = false;
        _fadeText.enabled = false;
    }

    IEnumerator FadeOut()
    {
        _fadeImage.enabled = true;
        _fadeText.enabled = true;

        while (_instance._fadeImage.color.a < 1)
        {
            Color color = _instance._fadeImage.color;
            color.a += Time.deltaTime;
            _instance._fadeImage.color = color;
            yield return null;
        }
    }
}
