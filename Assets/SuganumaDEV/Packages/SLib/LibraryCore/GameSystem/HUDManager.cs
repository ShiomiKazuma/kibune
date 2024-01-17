using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auth suganuma
namespace SLib
{
    namespace Systems
    {
        /// <summary> list “o˜^‚³‚ê‚½HUD‚ğŠÇ—‚·‚é </summary>
        public class HUDManager : MonoBehaviour     // list ÅŒã”ö‚ªˆê”ÔŒã‚ë
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
