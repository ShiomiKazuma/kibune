// �Ǘ��� ����
using System;
using System.Linq;
using UnityEngine;
namespace SLib
{
    namespace AI
    {
        /// <summary> ���؂̍��W�����i�[���Ă��� </summary>
        public class PathHolder : MonoBehaviour
        {
            [SerializeField, Header("Path Color")]
            Color color = Color.yellow;
            [SerializeField, Header("Marker Color")]
            Color markerC = Color.cyan;
            [SerializeField, Header("Point Marker Size")]
            float markerSize = 1.0f;
            /// <summary> AI�̃p�g���[�����铹�؂̊e����_�̃g�����X�t�H�[����Ԃ� </summary>
            /// <returns></returns>
            public Vector3[] GetPatrollingPath()
            {
                var temp = Array.ConvertAll(transform.GetComponentsInChildren<Transform>(), x => x.position).ToList();
                temp.RemoveAt(0);
                return temp.ToArray();
            }

            private void OnDrawGizmos()
            {
                var points = transform.GetComponentsInChildren<Transform>();
                var work = points.ToList();
                work.RemoveAt(0);
                points = work.ToArray();
                Gizmos.color = markerC;
                foreach (var p in points)
                {
                    Gizmos.DrawCube(p.position, Vector3.one * markerSize);
                } // draw sphere to each point's position
                Gizmos.color = color;
                Gizmos.DrawLineStrip(Array.ConvertAll(points, x => x.position), true);
            }
        }
    }
}