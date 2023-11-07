using System;
using UnityEngine;
namespace RSEngine
{
    namespace AI
    {
        public class PartollingPathHolder : MonoBehaviour
        {
            /// <summary> AI�̃p�g���[�����铹�؂̊e����_�̃g�����X�t�H�[����Ԃ� </summary>
            /// <returns></returns>
            public Transform[] GetPatrollingPath() { return transform.GetComponentsInChildren<Transform>(); }
            private void OnDrawGizmos()
            {
                var points = transform.GetComponentsInChildren<Transform>();
                Gizmos.color = Color.yellow;
                foreach (var p in points)
                {
                    Gizmos.DrawWireSphere(p.position, .5f);
                } // draw sphere to each point's position
                Gizmos.DrawLineStrip(Array.ConvertAll(points, x => x.position), true);
            }
        }
    }
}