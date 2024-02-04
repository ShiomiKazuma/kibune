using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SLib.Systems;
// �쐬 ����
namespace SLib
{
    namespace Systems
    {
        public class PlayerSaveDataSerializer : SingletonBaseClass<PlayerSaveDataSerializer> // �Z�[�u�f�[�^�̓W�J
        {
            protected override void ToDoAtAwakeSingleton() { }

            public SaveDataTemplate ReadSaveData()
            {
                string dataStr = "";
                try
                {
                    StreamReader sr = new StreamReader(Application.dataPath + "/PlayerSavedData.json");
                    dataStr = sr.ReadToEnd();
                    sr.Close();
                    return JsonUtility.FromJson<SaveDataTemplate>(dataStr);
                }
                catch(FileNotFoundException)
                {
                    var t = GameObject.FindGameObjectWithTag("Player_Pos_OnNoData").transform;
                    var data = new SaveDataTemplate();
                    data._lastStandingRotation = t.rotation;
                    data._lastStandingPosition = t.position;

                    return data;
                }
            }
        }

    }
}
