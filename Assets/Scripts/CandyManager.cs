using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI�֘A�̃R���|�[�l���g���g�p���邽�߂ɕK�v

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 1;
    public int candy = DefaultCandyAmount;
    int counter;
    public int score = 0;
    private bool isGameOver = false;  // �Q�[���I�[�o�[��Ԃ��Ǘ�

    public Button restartButton; // Button�R���|�[�l���g���Q��

    void Start()
    {
        // �Q�[���J�n���Ƀ{�^�����\���ɂ���
        restartButton.gameObject.SetActive(false);  // �Q�[���J�n���ɂ̓{�^�����\��
        restartButton.onClick.AddListener(RestartGame);  // �{�^�����N���b�N���ꂽ�Ƃ���RestartGame���\�b�h���Ăяo��
    }

    public void ConsumeCandy()
    {
        // �Q�[���I�[�o�[��Ԃł̓L�����f�B������ł��Ȃ�
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

        // �Q�[���I�[�o�[���ɕ\�����郁�b�Z�[�W
        if (isGameOver)
        {
            GUI.Label(new Rect(50, 100, 200, 30), "Game Over!");
            restartButton.gameObject.SetActive(true); // �Q�[���I�[�o�[��Ƀ{�^����\��
        }
    }

    void Update()
    {
        // �Q�[���I�[�o�[��Ԃł͏������s��Ȃ�
        if (isGameOver) return;

        // �X�R�A��-30����������ꍇ�ɃQ�[���I�[�o�[
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
        restartButton.gameObject.SetActive(true); // �Q�[���I�[�o�[��Ƀ{�^����\��
    }

    // �Q�[�������X�^�[�g���郁�\�b�h
    public void RestartGame()
    {
        score = 0;             // �X�R�A�����Z�b�g
        candy = DefaultCandyAmount;  // �L�����f�B�̐������Z�b�g
        isGameOver = false;    // �Q�[���I�[�o�[�t���O������
        counter = 0;           // �J�E���g�_�E�������Z�b�g
        restartButton.gameObject.SetActive(false); // �{�^�����\���ɂ���

        // �K�v�ɉ����āA�V�[���������[�h���鏈����ǉ��ł��܂��B
        // �Ⴆ�΁A�V�[���������[�h����ꍇ:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
