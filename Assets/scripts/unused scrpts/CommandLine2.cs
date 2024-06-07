using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CommandLine2 : MonoBehaviour
{
    public Text commandText; // ��������� ���� ��� ����������� ������
    public Text feedbackText; // ��������� ���� ��� ����������� �������� �����
    public NavMeshAgent agent; // NavMeshAgent ��� �������� � ������
    public GameObject cubeToGo; // �����, � �������� ����� ����

    private string currentCommand = ""; // ������� �������
    private bool commandEntered = false; // ���� ��� ��������� �������� �������
    private float feedbackDisplayDuration = 2f; // ����� ����������� �������� ����� � ������������ ������� � ��������
    private float feedbackDisplayTimer = 0f; // ������ ��� ������������ ������� ����������� �������� ����� � ������������ �������

    void Start()
    {
        UpdateCommandLineText(); // ��������� ����� ��������� ������
    }

    void Update()
    {
        // �������� ������� �������
        string keyPressed = Input.inputString;

        // ���� ������ ������� Enter
        if (keyPressed == "\n" || keyPressed == "\r")
        {
            if (!commandEntered)
            {
                ExecuteCommand(); // ��������� �������
                commandEntered = true; // ������������� ����, ����� �� ������������ ������� ��������
            }
        }
        else
        {
            // ���� ������ ������� Backspace, ������� ��������� ������
            if (keyPressed == "\b" && currentCommand.Length > 0)
            {
                currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
            }
            else
            {
                // ��������� ������ � ������� �������
                currentCommand += keyPressed;
            }

            // ��������� ����� ��������� ������
            UpdateCommandLineText();

            // �������� ����� �������� �����
            HideFeedback();

            commandEntered = false; // ���������� ���� ��� ����� ������ �������
        }
    }

    // ����� ��� ���������� ������ � ��������� ������
    void UpdateCommandLineText()
    {
        commandText.text = "> " + currentCommand; // ��������� ����� � ��������� ����
    }

    // ����� ��� ���������� �������
    void ExecuteCommand()
    {
        // ��������� ������� �������
        string currentCommandText = currentCommand;

        // ������� ������� �������
        currentCommand = "";

        // ��������� ����� ��������� ������
        UpdateCommandLineText();

        // ���� ������� ������� "go to the cube"
        if (currentCommandText == "go to the cube")
        {
            // ��������� NavMeshAgent ��� �������� � ������
            agent.SetDestination(cubeToGo.transform.position);

            // �������� ����� �������� �����
            HideFeedback();
        }
        else // ���� ������� ������������ �������
        {
            // ���������� ����� "unknown command"
            ShowFeedback("unknown command");
        }
    }

    // ����� ��� ����������� ������ �������� �����
    void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        feedbackDisplayTimer = feedbackDisplayDuration; // ������������� ������ ������ ����������������� �����������
    }

    // ����� ��� ������� ������ �������� �����
    void HideFeedback()
    {
        if (feedbackDisplayTimer > 0f) // ���������, ��� ������ ��� �� �����
        {
            feedbackDisplayTimer -= Time.deltaTime; // ��������� ������
            if (feedbackDisplayTimer <= 0f) // ���� ����� �������
            {
                feedbackText.gameObject.SetActive(false); // �������� ����� �������� �����
            }
        }
    }
}
