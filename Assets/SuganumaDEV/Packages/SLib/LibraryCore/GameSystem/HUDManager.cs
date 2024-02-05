using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auth suganuma
namespace SLib
{
    namespace Systems
    {
        /// <summary> list �o�^���ꂽHUD���Ǘ����� </summary>
        public class HUDManager : SingletonBaseClass<HUDManager>     // list �Ō������Ԍ��
        {
            [SerializeField, Header("�eHUD�̐e�I�u�W�F�N�g")]
            GameObject _allHUDParent;
            [SerializeField, Header("�eHUD���W���[�� \n�����W���[�����{�Œǉ����邱�ƁB��΂ɕ��ѕς��Ȃ���")]
            List<GameObject> _huds;

            public List<GameObject> HUDs => _huds;

            SceneLoader _sceneLoader;

            public void ToFront(int index)
            {
                var huds = _allHUDParent.GetChildObjects();
                for (int i = 0; i < _allHUDParent.transform.childCount; i++)
                {
                    huds[i].gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
                }
                _huds[index].transform.SetAsLastSibling();
                _huds[index].gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
            }

            public void KillAll()
            {
                var huds = _allHUDParent.GetChildObjects();
                foreach (var hud in huds)
                {
                    hud.gameObject.SetActive(false);
                }
            }

            protected override void ToDoAtAwakeSingleton()
            {
                GameObject.DontDestroyOnLoad(_allHUDParent);
                _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
            }
        }
    }
}
