using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLib
{
    namespace Systems
    {
        [CreateAssetMenu(fileName = "DependingEachObjects", menuName = "DependingEachObjects", order = 1)]
        /// <summary> リストに登録されたプレハブをシーン上にまとめて登場させることを強制するひな形 </summary>
        public class ObjectsDependingEachOther : ScriptableObject
        {
            public List<GameObject> objects = new List<GameObject>();
        }
    }
}
