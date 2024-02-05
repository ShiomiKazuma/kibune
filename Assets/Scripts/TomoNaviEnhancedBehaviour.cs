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
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // まずとも宅へ
        // Dialogue 01 ストーリー進行 カメラ回収へ
    }

    // ↓ カメラを集めに行く

    // ↓ カメラを集めたらナビからこれの呼び出し
    public void InvokeEventOnCatchCamera()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_01");
        var p2 = GameObject.Find("Tomo_Dialogue_02");
        p1.SetActive(false);
        p2.SetActive(true);
        // ↓ ともにカメラを解析させるてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // ↓ Dialogue 02 で ストーリー進行 凶器回収へ
    }

    // ↓ 凶器を集めに行く

    // ↓ 凶器を集めたら友ナビからこれの呼び出し
    public void InvokeEventOnCatchKnife()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_02");
        var p2 = GameObject.Find("Tomo_Dialogue_03");
        p1.SetActive(false);
        p2.SetActive(true);
        // ↓ ともの解析が終わったてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // 友ナビ ポップ → ともから「解析が終わった。面白いなこれ」
        // ↓ Dialogue 03 で ストーリー 進行
    }

    // ↓ 写真を回収して真実を知る

    public void InvokeEventOnCatchPicture()
    {

    }
}
