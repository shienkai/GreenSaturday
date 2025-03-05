using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI関連のコンポーネントを使用するために必要

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    public int score = 0;
    private bool isGameOver = false;  // ゲームオーバー状態を管理

    public Button restartButton; // Buttonコンポーネントを参照

    void Start()
    {
        // ゲーム開始時にボタンを非表示にする
        restartButton.gameObject.SetActive(false);  // ゲーム開始時にはボタンを非表示
        restartButton.onClick.AddListener(RestartGame);  // ボタンがクリックされたときにRestartGameメソッドを呼び出す
    }

    public void ConsumeCandy()
    {
        // ゲームオーバー状態ではキャンディを消費できない
        if (isGameOver || candy <= 0) return;

        candy--;
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

        GUI.Label(new Rect(50, 50, 100, 30), label);
        GUI.Label(new Rect(50, 30, 100, 20), ScoreLabel);

        // ゲームオーバー時に表示するメッセージ
        if (isGameOver)
        {
            GUI.Label(new Rect(50, 100, 200, 30), "Game Over!");
            restartButton.gameObject.SetActive(true); // ゲームオーバー後にボタンを表示
        }
    }

    void Update()
    {
        // ゲームオーバー状態では処理を行わない
        if (isGameOver) return;

        // スコアが-30を下回った場合にゲームオーバー
        if (score < -30)
        {
            GameOver();
        }

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

    void GameOver()
    {
        isGameOver = true;
        restartButton.gameObject.SetActive(true); // ゲームオーバー後にボタンを表示
    }

    // ゲームをリスタートするメソッド
    public void RestartGame()
    {
        score = 0;             // スコアをリセット
        candy = DefaultCandyAmount;  // キャンディの数をリセット
        isGameOver = false;    // ゲームオーバーフラグを解除
        counter = 0;           // カウントダウンをリセット
        restartButton.gameObject.SetActive(false); // ボタンを非表示にする

        // 必要に応じて、シーンをリロードする処理を追加できます。
        // 例えば、シーンをリロードする場合:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
