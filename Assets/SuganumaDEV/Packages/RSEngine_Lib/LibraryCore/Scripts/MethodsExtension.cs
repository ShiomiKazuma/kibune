// �Ǘ��� ����
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace RSEngine
{
    /// <summary> �g�����\�b�h��񋟂���N���X </summary>
    public static class MethodsExtension
    {
        /* GameObjects */
        /// <summary>�w�肳�ꂽ�g�����X�t�H�[���̎q�I�u�W�F�N�g�ɂ���</summary>
        /// <param name="obj"></param>
        /// <param name="parent"></param>
        public static void ToChildObject(this GameObject obj, Transform parent)
        {
            obj.transform.parent = parent;
        }
        /// <summary>�I�u�W�F�N�g�̐e�q�֌W��؂�</summary>
        /// <param name="obj"></param>
        public static void ToParenObject(this GameObject obj)
        {
            obj.transform.parent = null;
        }
        /// <summary>�q�I�u�W�F�N�g�̂ݎ擾����</summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Transform> GetChildObjects(this GameObject parent)
        {
            List<Transform> list = new();
            var cnt = parent.transform.childCount;
            for (int i = 0; i < cnt; i++)
            {
                var child = parent.transform.GetChild(i);
                list.Add(child);
            }
            return list;
        }
    }
}