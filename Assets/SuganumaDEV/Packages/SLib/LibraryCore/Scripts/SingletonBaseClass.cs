// �Ǘ��� ����
using System;
using UnityEngine;
namespace SLib
{
    namespace Singleton
    {
        /// <summary> ���̃N���X���p�����邱�Ƃɂ���āA�V���O���g���p�^�[���̃r�w�C�r�A�̋@�\��񋟂��� </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class SingletonBaseClass<T> : MonoBehaviour where T : Component
        {
            static T Instance;
            public static T SingletonInstance => Instance;
            protected void Awake()
            {
                if (Instance != null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Instance = this as T;
                    DontDestroyOnLoad(gameObject);
                    ToDoAtAwakeSingleton();
                }
            }
            /// <summary> Awake�֐����ŌĂяo���Ăق������������� </summary>
            protected abstract void ToDoAtAwakeSingleton();
        }
    }
}