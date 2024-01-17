using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            [SerializeField, Header("�eHUD���W���[�� �����W���[�����{�Œǉ����邱�ƁB��΂ɕ��ѕς��Ȃ���")]
            List<GameObject> _huds;

            SceneLoader _sceneLoader;

            public void ToFront(int index)
            {
                var huds = _allHUDParent.GetChildObjects();
                for (int i = 0; i < _allHUDParent.transform.childCount; i++)
                {
                    huds[i].gameObject.SetActive(false);
                }
                _huds[index].transform.SetAsLastSibling();
                _huds[index].gameObject.SetActive(true);
            }

            protected override void ToDoAtAwakeSingleton()
            {
                GameObject.DontDestroyOnLoad(_allHUDParent);
                _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
            }
        }
    }
}
