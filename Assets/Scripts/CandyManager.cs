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
    
    // ���̃X�R�A�ƕ��̃X�R�A
    public int positiveScore = 0;
    public int negativeScore = 0;

    private bool isGameOver = false;

    public Button restartButton;
    public Shooter shooter;

    void Start()
    {
        restartButton.gameObject.SetActive(false);  // �Q�[���J�n���ɂ̓{�^�����\��
        restartButton.onClick.AddListener(OnRestartButtonClicked);  // �{�^���̃N���b�N���X�i�[��ݒ�
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

    // ���̃X�R�A��ǉ�
    public void AddPositiveScore(int point)
    {
        positiveScore += point;
    }

    // ���̃X�R�A��ǉ�
    public void AddNegativeScore(int point)
    {
        negativeScore += point;
    }

    void OnGUI()
    {
        GUI.color = Color.black;

        string label = "Candy :" + candy;
        if (counter > 0) label = label + " (" + counter + "s)";
        string positiveScoreLabel = "�擾�P�ʐ� :" + positiveScore;
        string negativeScoreLabel = "�s�� :" + negativeScore;

        GUI.Label(new Rect(50, 50, 200, 30), label);
        GUI.Label(new Rect(50, 10, 200, 30), positiveScoreLabel);
        GUI.Label(new Rect(50, 30, 200, 30), negativeScoreLabel);

        if (isGameOver)
        {
            GUI.Label(new Rect(50, 100, 200, 30), "Game Over!");
            restartButton.gameObject.SetActive(true);  // �Q�[���I�[�o�[��Ƀ{�^����\��
        }
    }

    void Update()
    {
        if (isGameOver) return;

        // ���̃X�R�A�� -2 ��菭�Ȃ��ꍇ�ɃQ�[���I�[�o�[
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
        restartButton.gameObject.SetActive(true);  // �Q�[���I�[�o�[��Ƀ{�^����\��

        if (shooter != null)
        {
            shooter.StopShooting();  // �Q�[���I�[�o�[���ɔ��˂��~
        }
    }

    // �V�[�������[�h���܂߂����X�^�[�g����
    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);  // �Ⴆ��1�b�̒x��
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // ���݂̃V�[�����ēǂݍ���
    }

    void OnRestartButtonClicked()
    {
        StartCoroutine(RestartGame());  // �R���[�`���̌Ăяo��
    }
}
