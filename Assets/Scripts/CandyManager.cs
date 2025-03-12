using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �V�[���Ǘ��̂��߂ɒǉ�

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    public int score = 0;
    private bool isGameOver = false;

    public Button restartButton;
    public Shooter shooter;  // Shooter �X�N���v�g���Q��

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
            restartButton.gameObject.SetActive(true);  // �Q�[���I�[�o�[��Ƀ{�^����\��
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
        restartButton.gameObject.SetActive(true);  // �Q�[���I�[�o�[��Ƀ{�^����\��

        if (shooter != null)
        {
            shooter.StopShooting();  // �Q�[���I�[�o�[���ɔ��˂��~
        }
    }

    // �V�[�������[�h���܂߂����X�^�[�g����
    public IEnumerator RestartGame()
    {
        // �Q�[���I�[�o�[�������ɉ����x�������������ꍇ�Ɏg����
        yield return new WaitForSeconds(1f);  // �Ⴆ��1�b�̒x��

        // �V�[�������[�h (���݂̃V�[�����ēǂݍ���)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��ɌĂ΂�郉�b�v���\�b�h
    void OnRestartButtonClicked()
    {
        StartCoroutine(RestartGame());  // �R���[�`���̌Ăяo��
    }
}
