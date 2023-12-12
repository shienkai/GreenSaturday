using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    public int score = 0;

    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }

    public int GetCandyAmount()
    {
        return candy;
    }

    public void AddCandy(int amount)
    {
        candy += amount;
    }

    public void AddScore(int point)
    {
        score += point;
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        
        string label = "Candy :" + candy;

        if (counter > 0) label = label + " ("+ counter + "s)";

        string ScoreLabel = "Score :" + score;

        GUI.Label(new Rect(50,50,100,30),label);

        GUI.Label(new Rect(50, 30, 100, 20), ScoreLabel);
    }
    void Update()
    {
        if (candy < DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
    }
    IEnumerator RecoverCandy()
    {
        counter = RecoverySeconds;

        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        candy++;
    }
}