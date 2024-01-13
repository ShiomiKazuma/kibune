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

    [Header("�t�F�[�h�A�E�g�Ɏg���p�l��")]
    [SerializeField]
    private Graphic _fadePanel;
    [SerializeField] Text _LoadText;

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
        yield return LoadText();
        SceneManager.LoadScene(name);
        yield return FadeIn();
    }

    /// <summary>
    /// �ꉞ�O������Ăяo����悤�ɂ��Ă����t�F�[�h�C���p�̃R���[�`��
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
            Debug.LogError("�t�F�[�h�A�E�g�Ɏg���p�l�����ݒ肳��Ă��܂���");
            yield break;
        }

        Debug.Log("�t�F�[�h�C������");
    }

    /// <summary>
    /// �O����Ăяo���V�[���J�ڗp�̃R���[�`��
    /// </summary>
    IEnumerator FadeOut(float duration = 0.4f)
    {
        // DOFade�ɂ��t�F�[�h�A�E�g���s��
        if (_fadePanel != null)
        {
            _fadePanel.gameObject.SetActive(true);
            yield return _fadePanel.DOFade(1.0f, duration).WaitForCompletion();
        }
        else
        {
            Debug.LogError("�t�F�[�h�A�E�g�Ɏg���p�l�����ݒ肳��Ă��܂���");
        }
    }

    IEnumerator LoadText(float duration = 1.0f)
    {
        if (_LoadText != null)
        {
            _LoadText.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            _LoadText.gameObject.SetActive(false) ;
        }
        else
        {
            Debug.Log("���[�h��ʂɏo���e�L�X�g���ݒ肳��Ă��܂���");
        }
    }

    /// <summary>
    /// �{�^������Ăяo���p�̃V�[���J�ڗp�̃��\�b�h
    /// </summary>
    public void ChangeScene(string name)
    {
        StartCoroutine(Execute(name));
    }
}