using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLib
{
    namespace Systems
    {
        [CreateAssetMenu(fileName = "DependingEachObjects", menuName = "DependingEachObjects", order = 1)]
        /// <summary> ���X�g�ɓo�^���ꂽ�v���n�u���V�[����ɂ܂Ƃ߂ēo�ꂳ���邱�Ƃ���������ЂȌ` </summary>
        public class ObjectsDependingEachOther : ScriptableObject
        {
            public List<GameObject> objects = new List<GameObject>();
        }
    }
}
