using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SLib
{
    namespace Systems
    {
        /// <summary> ���X�g�ɓo�^���ꂽ�v���n�u���V�[����ɂ܂Ƃ߂ēo�ꂳ���邱�Ƃ��������� </summary>
        public class DependingEachObjectsManager : SingletonBaseClass<DependingEachObjectsManager> 
        {
            [SerializeField] ObjectsDependingEachOther _objects;

            protected override void ToDoAtAwakeSingleton()
            {
                
            }

            private void OnEnable()
            {
                foreach (var item in _objects.objects)
                {
                    if (GameObject.Find(item.name) == null) GameObject.Instantiate(item);
                }
            }
        }
    }
}
