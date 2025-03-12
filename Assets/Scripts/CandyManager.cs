using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン管理のために追加

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    public int score = 0;
    private bool isGameOver = false;

    public Button restartButton;
    public Shooter shooter;  // Shooter スクリプトを参照

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

    public void AddScore(int point)
    {
        score += point;
    }

    void OnGUI()
    {
        GUI.color = Color.black;

        string label = "Candy :" + candy;
        if (counter > 0) label = label + " (" + counter + "s)";
        string ScoreLabel = "Score :" + score;

        GUI.Label(new Rect(50, 50, 100, 30), label);
        GUI.Label(new Rect(50, 30, 100, 20), ScoreLabel);

        if (isGameOver)
        {
            GUI.Label(new Rect(50, 100, 200, 30), "Game Over!");
            restartButton.gameObject.SetActive(true);  // ゲームオーバー後にボタンを表示
        }
    }

    void Update()
    {
        if (isGameOver) return;

        if (score < -2)
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
        // ゲームオーバー処理中に何か遅延を加えたい場合に使える
        yield return new WaitForSeconds(1f);  // 例えば1秒の遅延

        // シーンリロード (現在のシーンを再読み込み)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ボタンがクリックされたときに呼ばれるラップメソッド
    void OnRestartButtonClicked()
    {
        StartCoroutine(RestartGame());  // コルーチンの呼び出し
    }
}
