using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentakuDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public int scorereward;

    // �Փ˂����I�u�W�F�N�g�̃^�O���g���Ĕ���
    public string positiveZoneTag = "PositiveZone"; // ���̃X�R�A�]�[���̃^�O
    public string negativeZoneTag = "NegativeZone"; // ���̃X�R�A�]�[���̃^�O

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sentaku")
        {
            candyManager.AddCandy(reward);

            // �����蔻��Ő��������߂�
            if (other.gameObject.CompareTag(positiveZoneTag))
            {
                candyManager.AddPositiveScore(scorereward);  // ���̃X�R�A
            }
            else if (other.gameObject.CompareTag(negativeZoneTag))
            {
                candyManager.AddNegativeScore(scorereward);  // ���̃X�R�A
            }

            Destroy(other.gameObject);
        }
    }
}
