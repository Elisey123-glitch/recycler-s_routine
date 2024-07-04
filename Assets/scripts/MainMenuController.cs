using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text commandText; // ��������� ���� ��� ����������� ������
    public Text feedbackText; // ��������� ���� ��� ����������� �������� �����
    public TextReveal textReveal; // ������ �� ��������� TextReveal
    public string sceneToLoad; // ��� ����� ��� ��������

    public string startCommandText = "Enter password from mailbox to run the program";
    public string wrongPasswordText = "Wrong password, try again";
    public string mailboxText = "Mailbox opened. Your password is 1234567.";
    public string exitingGameText = "Exiting game...";
    public string volumeSettingsText = "Volume settings:\nEnter a value between 0 and 10 to set the volume.";

    public AudioSource backgroundMusic; // ������ �� ������� ������

    private string currentCommand = ""; // ������� �������
    private bool commandEntered = false; // ���� ��� ��������� ��������� �������
    private float feedbackDisplayDuration = 1f; // ����� ����������� �������� ����� � ��������
    private float feedbackDisplayTimer = 0f; // ������ ��� ������������ ������� ����������� �������� �����

    private bool isWaitingForPassword = false;
    private bool isWaitingForVolume = false;
    private int passwordAttempts = 0;
    private const int maxPasswordAttempts = 3;
    private const string correctPassword = "1234567";
    private string[] initialCommands = { "/start", "/options", "/mailbox", "/exit" };
    private string initialCommandsText;

    void Start()
    {
        initialCommandsText = string.Join("\n", initialCommands);
        commandText.text = initialCommandsText;
    }

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
        commandText.text = "/ " + currentCommand;
    }

    void ExecuteCommand()
    {
        string command = currentCommand.ToLower().Trim();
        currentCommand = "";
        UpdateCommandLineText();

        if (isWaitingForPassword)
        {
            CheckPassword(command);
        }
        else if (isWaitingForVolume)
        {
            SetVolume(command);
        }
        else if (command == "start")
        {
            PromptForPassword();
        }
        else if (command == "exit")
        {
            ExitGame();
        }
        else if (command == "mailbox")
        {
            OpenMailbox();
        }
        else if (command == "options")
        {
            ShowVolumeSettings();
        }
        else
        {
            ShowFeedback("Unknown command");
        }
    }

    void PromptForPassword()
    {
        isWaitingForPassword = true;
        commandText.text = ""; // �������� ����� ����� ���������
        textReveal.RevealNewText(startCommandText);
        Invoke("ResetCommandText", 5f); // ����� ������ ����� 5 ������
    }

    void CheckPassword(string password)
    {
        if (password == correctPassword)
        {
            StartGame();
        }
        else
        {
            passwordAttempts++;
            if (passwordAttempts >= maxPasswordAttempts)
            {
                ExitGame();
            }
            else
            {
                commandText.text = ""; // �������� ����� ����� ���������
                textReveal.RevealNewText(wrongPasswordText);
                Invoke("ResetCommandText", 5f); // ����� ������ ����� 5 ������
            }
        }
    }

    void ResetCommandText()
    {
        isWaitingForPassword = false;
        isWaitingForVolume = false;
        textReveal.RevealNewText(initialCommandsText); // �������� �������� � ���������� ������
    }

    void OpenMailbox()
    {
        commandText.text = ""; // �������� ����� ����� ���������
        textReveal.RevealNewText(mailboxText);
        Invoke("ResetCommandText", 5f); // ����� ������ ����� 5 ������
    }

    void ShowVolumeSettings()
    {
        isWaitingForVolume = true;
        commandText.text = ""; // �������� ����� ����� ���������
        textReveal.RevealNewText(volumeSettingsText);
        Invoke("ResetCommandText", 9f); // ����� ������ ����� 9 ������
    }

    void SetVolume(string command)
    {
        if (int.TryParse(command, out int volume))
        {
            if (volume >= 0 && volume <= 10)
            {
                backgroundMusic.volume = volume / 10f;
                ShowFeedback("Volume set to " + volume);
            }
            else
            {
                ShowFeedback("Invalid volume. Enter a value between 0 and 10.");
            }
        }
        else
        {
            ShowFeedback("Invalid input. Enter a number between 0 and 10.");
        }
    }

    void StartGame()
    {
        ShowFeedback("Starting game...");
        SceneManager.LoadScene(sceneToLoad);
    }

    void ExitGame()
    {
        commandText.text = ""; // �������� ����� ����� ���������
        textReveal.RevealNewText(exitingGameText);
        Invoke("QuitApplication", 2f); // �������� ����� ������� �� ����
    }

    void QuitApplication()
    {
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
