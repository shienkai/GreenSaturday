using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentakuDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public int scorereward;

    // 衝突したオブジェクトのタグを使って判定
    public string positiveZoneTag = "PositiveZone"; // 正のスコアゾーンのタグ
    public string negativeZoneTag = "NegativeZone"; // 負のスコアゾーンのタグ

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sentaku")
        {
            candyManager.AddCandy(reward);

            // 当たり判定で正負を決める
            if (other.gameObject.CompareTag(positiveZoneTag))
            {
                candyManager.AddPositiveScore(scorereward);  // 正のスコア
            }
            else if (other.gameObject.CompareTag(negativeZoneTag))
            {
                candyManager.AddNegativeScore(scorereward);  // 負のスコア
            }

            Destroy(other.gameObject);
        }
    }
}
