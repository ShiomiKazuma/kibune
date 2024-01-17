using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auth suganuma
namespace SLib
{
    namespace Systems
    {
        /// <summary> list �o�^���ꂽHUD���Ǘ����� </summary>
        public class HUDManager : MonoBehaviour     // list �Ō������Ԍ��
        {
            [SerializeField]
            List<GameObject> _huds;

            public void ToFront(int index)
            {
                _huds[index].transform.SetAsLastSibling();
            }
                
        }
    }
}
