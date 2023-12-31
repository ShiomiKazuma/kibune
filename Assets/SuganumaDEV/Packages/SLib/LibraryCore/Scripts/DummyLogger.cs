// 管理者 菅沼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SLib
{
    enum LoggingMode
    {
        Normal,
        Warning,
        Error,
    }
    /// <summary> 渡された文字列をただDebugログへ出力する。 </summary>
    public class DummyLogger : MonoBehaviour
    {
        [SerializeField] LoggingMode _mode;
        public void DummyLoggerOutputLog(string message)
        {
            switch (_mode)
            {
                case LoggingMode.Warning:
                    Debug.LogWarning(message);
                    break;
                case LoggingMode.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}