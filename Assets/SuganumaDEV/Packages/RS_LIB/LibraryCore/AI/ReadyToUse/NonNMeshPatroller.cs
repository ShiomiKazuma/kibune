using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace RSEngine
{
    namespace AI
    {
        /// <summary> NavMeshAgent ���g�p���Ȃ��A����o�H�̕K�v�ȁh����ҁhAI�@�\��񋟂��� </summary>
        [RequireComponent(typeof(Rigidbody))]
        public class NonNMeshPatroller : MonoBehaviour
        {
            Rigidbody _rb;
            [SerializeField, Range(1f, 50f)] float _targetDetectRangeRadius;
            [SerializeField, Range(0, 5)] float _targetDetectBufferRangeRadius;
            [SerializeField, Range(1f, 50f)] float _attackingRangeRadius;
            [SerializeField, Range(1f, 100f)] float _moveSpeed;
            [SerializeField, Range(1f, 10f)] float _fallSpeed;
            [SerializeField] Transform[] _patrollPath;
            [SerializeField] List<GameObject> _targetsList = new();
            [SerializeField] LayerMask _targetObjectLayer;
            [SerializeField] LayerMask _groundLayer;
            [SerializeField] int _targetsLayerNumber;
            [SerializeField] UnityEvent _attackTargetSeq;
            int _currentTargetIndex = 0;
            int _currentPatrollPathIndex = 0;
            bool _isGrounded = false;
            void AddGravityToThis() => _rb.AddForce((!_isGrounded) ? -transform.up * _fallSpeed * 100 : Vector3.zero);
            /// <summary> �ڒn���o </summary>
            bool CheckGrounded()
            {
                return Physics.Raycast(transform.position, -transform.up, 1f, _groundLayer);
            }
            bool IsTargetInLevel()
            {
                return GameObject.FindObjectsOfType<GameObject>().Where(x => x.layer == _targetsLayerNumber).ToList().Count > 0;
            }
            /// <summary> �ǐՉ\�Ȃ��ׂĂ̓G�̌��� </summary>
            List<GameObject> DetectChasableTargets()
            {
                return GameObject.FindObjectsOfType<GameObject>().Where(x => x.layer == _targetsLayerNumber).ToList();
            }
            /// <summary> �C�ӂ̃I�u�W�F�N�g�����m�����ɂ��邩���� </summary>
            /// <param name="start"> �n�_ </param>
            /// <param name="end"> �I�_ </param>
            /// <param name="limitDistance"> ���m���a </param>
            /// <param name="limitOffset"> ���m�����덷���e�l </param>
            /// <returns></returns>
            bool CheckInsideBorder(Vector3 start, Vector3 end, float limitDistance, float limitOffset)// �����x�[�X���m
            {
                float dx = end.x - start.x;
                float dy = end.y - start.y;
                float dz = end.z - start.z;
                float dd = dx * dx + dy * dy + dz * dz;
                float lim = limitDistance * limitDistance;
                return dd < lim + limitOffset;
            }
            #region �s�����[�`��
            /// <summary> �n���ꂽ���؂����ǂ郋�[�`�� </summary>
            /// <param name="patrollPointIndex"></param>
            /// <param name="patrollPath"></param>
            void PatrollNow(int patrollPointIndex, Transform[] patrollPath)
            {
                var dir = (patrollPath[patrollPointIndex].position - transform.position).normalized;
                dir.y = 0;
                var v = dir * _moveSpeed;
                _rb.velocity = v;
                var b = CheckInsideBorder(transform.position, patrollPath[patrollPointIndex].position, 1, 1);
                // Debug.Log($"{gameObject.name}:{this.GetType()}:inside border-{b},index-{patrollPointIndex},point-{patrollPath[patrollPointIndex]}");
                if (b) { _currentPatrollPathIndex = (_currentPatrollPathIndex + 1 < _patrollPath.Length) ? _currentPatrollPathIndex + 1 : 0; }
            }
            /// <summary> �n���ꂽ�I�u�W�F�N�g��ǐՂ��郋�[�`�� </summary>
            /// <param name="isTargetInSight"></param>
            /// <param name="target"></param>
            void ChaseWithTarget(bool isTargetInSight, GameObject target)
            {
                if (isTargetInSight)
                {
                    var dir = (target.transform.position - transform.position).normalized;
                    dir.y = 0;
                    var v = dir * _moveSpeed;
                    _rb.velocity = v;
                }
            }
            void AttackToTarget(bool isTargetInRange, UnityEvent action)
            {
                if (isTargetInRange) action.Invoke();
            }
            #endregion
            private void Start()
            {
                _rb = GetComponent<Rigidbody>();
                _rb.freezeRotation = true;
                _targetsList = DetectChasableTargets();
                _targetsLayerNumber = (_targetsLayerNumber == 0) ? 1 : _targetsLayerNumber; // �o�O�̗\�h
            }
            void FixedUpdate()
            {
                _isGrounded = CheckGrounded();
                _targetsList = DetectChasableTargets();
                AddGravityToThis();

                if (IsTargetInLevel())
                {
                    var isDetectable = CheckInsideBorder(transform.position, _targetsList[_currentTargetIndex].transform.position, _targetDetectRangeRadius, _targetDetectBufferRangeRadius);
                    var isAttackable = CheckInsideBorder(transform.position, _targetsList[_currentTargetIndex].transform.position, _attackingRangeRadius, _targetDetectBufferRangeRadius);
                    ChaseWithTarget(isDetectable, _targetsList[_currentTargetIndex]);
                    AttackToTarget(isAttackable, _attackTargetSeq);
                }
                else
                {
                    Debug.Log("Target Is Not Found");
                    PatrollNow(_currentPatrollPathIndex, _patrollPath);
                }
            }
            void OnDrawGizmos()
            {
                // Sightable Target Detect Range
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, _targetDetectRangeRadius);
                // Sightable Attack Range
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _attackingRangeRadius);
                // Sightable Patroll Path
                Gizmos.color = Color.blue;
                Gizmos.DrawLineStrip(Array.ConvertAll(_patrollPath, x => x.position), true);
            }
        }
    }
}