using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheProofItem : MonoBehaviour
{
    enum RunStoryOnTaken
    {
        RunStory,
        None,
    }

    [SerializeField, Header("取得可能範囲"), Range(1, 5)]
    int range;
    [SerializeField, Header("インデックス"), Range(0, 2)]
    int index;
    [SerializeField, Header("回収時の友ナビ")]
    GameObject TomoNavi;
    [SerializeField, Header("Player's LayerMask")]
    LayerMask lMask;
    [SerializeField, Header("callback")]
    RunStoryOnTaken Hoge;

    public int Index => index;

    private void FixedUpdate()
    {
        var cond = Physics.CheckSphere(transform.position, range, lMask);
        if (cond)
        {
            var flag = GameObject.FindObjectOfType<FlagManager>();
            flag.AddActiveItem(index);
            //if (TomoNavi != null)
            //{
            //    var go = Instantiate(TomoNavi);
            //    go.transform.position = Vector3.zero;
            //}
            var man = GameObject.FindAnyObjectByType<FramedEventsInGameGeneralManager>();
            man.SaveData();
            man.TryGetSetProgressData();
            var tomNavMan = GameObject.FindFirstObjectByType<TomoNaviManager>();
            tomNavMan.PopNavi(TomoNavi);
            if (Hoge == RunStoryOnTaken.RunStory)
            {
                var prog = man.ReadSaveData().Finished;
                man.RunStory(prog);
            }
            Destroy(this.gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
