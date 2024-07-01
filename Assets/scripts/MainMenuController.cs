using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text commandText; // ��������� ���� ��� ����������� ������
    public Text feedbackText; // ��������� ���� ��� ����������� �������� �����
    public string sceneToLoad; // ��� ����� ��� ��������

    private string currentCommand = ""; // ������� �������
    private bool commandEntered = false; // ���� ��� ��������� ��������� �������
    private float feedbackDisplayDuration = 1f; // ����� ����������� �������� ����� � ��������
    private float feedbackDisplayTimer = 0f; // ������ ��� ������������ ������� ����������� �������� �����

    void Update()
    {
        HandleInput();
        HideFeedbackIfNeeded();
    }

    void HandleInput()
    {
        string keyPressed = Input.inputString;

        if (keyPressed == "\n" || keyPressed == "\r")
        {
            if (!commandEntered)
            {
                ExecuteCommand();
                commandEntered = true;
            }
        }
        else if (keyPressed == "\b" && currentCommand.Length > 0)
        {
            currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
        }
        else
        {
            currentCommand += keyPressed;
        }

        UpdateCommandLineText();
        commandEntered = false;
    }

    void UpdateCommandLineText()
    {
        commandText.text = "> " + currentCommand;
    }

    void ExecuteCommand()
    {
        string command = currentCommand.ToLower().Trim();
        currentCommand = "";
        UpdateCommandLineText();

        if (command == "start")
        {
            StartGame();
        }
        else if (command == "exit")
        {
            ExitGame();
        }
        else
        {
            ShowFeedback("Unknown command");
        }
    }

    void StartGame()
    {
        ShowFeedback("Starting game...");
        SceneManager.LoadScene(sceneToLoad);
    }

    void ExitGame()
    {
        ShowFeedback("Exiting game...");
        Application.Quit();
    }

    public void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        feedbackDisplayTimer = feedbackDisplayDuration;
    }

    void HideFeedback()
    {
        feedbackText.gameObject.SetActive(false);
    }

    void HideFeedbackIfNeeded()
    {
        if (feedbackDisplayTimer > 0f)
        {
            feedbackDisplayTimer -= Time.deltaTime;
            if (feedbackDisplayTimer <= 0f)
            {
                HideFeedback();
            }
        }
    }
}
