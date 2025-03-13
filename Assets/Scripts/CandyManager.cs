using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    
    // 正のスコアと負のスコア
    public int positiveScore = 0;
    public int negativeScore = 0;

    private bool isGameOver = false;

    public Button restartButton;
    public Shooter shooter;

    void Start()
    {
        restartButton.gameObject.SetActive(false);  // ゲーム開始時にはボタンを非表示
        restartButton.onClick.AddListener(OnRestartButtonClicked);  // ボタンのクリックリスナーを設定
    }

    public void ConsumeCandy()
    {
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

    // 正のスコアを追加
    public void AddPositiveScore(int point)
    {
        positiveScore += point;
    }

    // 負のスコアを追加
    public void AddNegativeScore(int point)
    {
        negativeScore += point;
    }

    void OnGUI()
    {
        GUI.color = Color.black;

        string label = "Candy :" + candy;
        if (counter > 0) label = label + " (" + counter + "s)";
        string positiveScoreLabel = "取得単位数 :" + positiveScore;
        string negativeScoreLabel = "不可 :" + negativeScore;

        GUI.Label(new Rect(50, 50, 200, 30), label);
        GUI.Label(new Rect(50, 10, 200, 30), positiveScoreLabel);
        GUI.Label(new Rect(50, 30, 200, 30), negativeScoreLabel);

        if (isGameOver)
        {
            GUI.Label(new Rect(50, 100, 200, 30), "Game Over!");
            restartButton.gameObject.SetActive(true);  // ゲームオーバー後にボタンを表示
        }
    }

    void Update()
    {
        if (isGameOver) return;

        // 負のスコアが -2 より少ない場合にゲームオーバー
        if (negativeScore < -2)
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
        restartButton.gameObject.SetActive(true);  // ゲームオーバー後にボタンを表示

        if (shooter != null)
        {
            shooter.StopShooting();  // ゲームオーバー時に発射を停止
        }
    }

    // シーンリロードを含めたリスタート処理
    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);  // 例えば1秒の遅延
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 現在のシーンを再読み込み
    }

    void OnRestartButtonClicked()
    {
        StartCoroutine(RestartGame());  // コルーチンの呼び出し
    }
}
