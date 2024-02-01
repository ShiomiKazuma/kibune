using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FramedInGameEventProgressData
{
    /// #インベントリインデックス#
    /// 0 - 監視カメラ
    /// 1 - 凶器のナイフ
    /// 2 - グラップリングフック
    /// 3 - 証拠の捏造前の写真
}

public class FramedEventsInGame : MonoBehaviour
{
    /// ＃フロー＃
    /// 1．まず友人宅へ行く グラップリングフックを得る？（インベントリ追加）
    /// 2．監視カメラをもぐ → 友人宅へ（インベントリ追加）
    /// 3．並行して凶器を探しに（インベントリ＋）
    /// 4．友人宅へ本物の映像を解析させる → インベントリに証拠の写真＋
    /// 5. 実行犯へ接触
    /// 6．ラストシーンへ
    /// 


}
