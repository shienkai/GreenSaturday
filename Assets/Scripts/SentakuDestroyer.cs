using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentakuDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public int scorereward;


    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "Sentaku")
      {
         candyManager.AddCandy(reward);
     
         candyManager.AddScore(scorereward);

         Destroy(other.gameObject);
      }
    }
}
