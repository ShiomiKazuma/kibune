// �Ǘ��� ����
using System;
namespace SLib
{
    #region �Ǝ����\�b�h Original Methods
    /// <summary> �Ǝ��̃��\�b�h��񋟂���N���X </summary>
    public static class SLib
    {
        /// <summary> 
        /// <para>��P�������^�̎��̂ݑ�Q�����̏��������s���� </para>
        /// When 1st Argument is True, Do 2nd Arguments Process
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Knock(bool condition, Action action)
        {
            if (condition) { action(); }
        }
        /* ------------------------------------------------------------------ */
    }
    #endregion
}