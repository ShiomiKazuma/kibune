using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 友ナビの機能拡張クラス
public class TomoNaviEnhancedBehaviour : MonoBehaviour
{
    // 初回
    public void SetObjectiveToSafeHouse()
    {
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.Find("Tomo_Dialogue_01").transform;
        mapObj.SetTarget(dest);
        // まずとも宅へ ↓ 
        // ↓ Dialogue 01 ストーリー進行 カメラ回収へ
    }

    // ↓ カメラを集めに行く

    // ↓ カメラを集めたらナビからこれの呼び出し
    public void InvokeEventOnCatchCamera()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_01");
        var p2 = GameObject.Find("Tomo_Dialogue_02");
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
        // ↓ ともにカメラを解析させるてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // とも宅へ ↓ 
        // ↓ Dialogue 02 で ストーリー進行 凶器回収へ
    }

    // ↓ 凶器を集めに行く

    // ↓ 凶器を集めたら友ナビからこれの呼び出し
    public void InvokeEventOnCatchKnife()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_02");
        var p2 = GameObject.Find("Tomo_Dialogue_03");
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
        // ↓ ともの解析が終わったてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // 友ナビ ポップ → ともから「解析が終わった。面白いなこれ」
        var tnMan = GameObject.FindFirstObjectByType<TomoNaviManager>();
        tnMan.PopNavi(4);
        // とも宅へ ↓ 
        // ↓ Dialogue 03 で ストーリー 進行
    }

    // ↓ 写真を回収して真実を知る

    // ↓ 写真回収時の友ナビからこれを呼び出す
    public void InvokeEventOnCatchPicture()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
        // ↓ ともの解析が終わったてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // 友ナビ ポップ → ともから「解析が終わった。面白いなこれ」
        // とも宅へ ↓ 
        // ↓ Dialogue 04 で ストーリー 進行
    }
}
