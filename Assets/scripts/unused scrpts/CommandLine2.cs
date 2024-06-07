using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CommandLine2 : MonoBehaviour
{
    public Text commandText; // Текстовое поле для отображения команд
    public Text feedbackText; // Текстовое поле для отображения обратной связи
    public NavMeshAgent agent; // NavMeshAgent для движения к кубику
    public GameObject cubeToGo; // Кубик, к которому нужно идти

    private string currentCommand = ""; // Текущая команда
    private bool commandEntered = false; // Флаг для обработки введённой команды
    private float feedbackDisplayDuration = 2f; // Время отображения обратной связи о неправильной команде в секундах
    private float feedbackDisplayTimer = 0f; // Таймер для отслеживания времени отображения обратной связи о неправильной команде

    void Start()
    {
        UpdateCommandLineText(); // Обновляем текст командной строки
    }

    void Update()
    {
        // Получаем нажатую клавишу
        string keyPressed = Input.inputString;

        // Если нажата клавиша Enter
        if (keyPressed == "\n" || keyPressed == "\r")
        {
            if (!commandEntered)
            {
                ExecuteCommand(); // Выполняем команду
                commandEntered = true; // Устанавливаем флаг, чтобы не обрабатывать команду повторно
            }
        }
        else
        {
            // Если нажата клавиша Backspace, стираем последний символ
            if (keyPressed == "\b" && currentCommand.Length > 0)
            {
                currentCommand = currentCommand.Substring(0, currentCommand.Length - 1);
            }
            else
            {
                // Добавляем символ к текущей команде
                currentCommand += keyPressed;
            }

            // Обновляем текст командной строки
            UpdateCommandLineText();

            // Скрываем текст обратной связи
            HideFeedback();

            commandEntered = false; // Сбрасываем флаг при вводе нового символа
        }
    }

    // Метод для обновления текста в командной строке
    void UpdateCommandLineText()
    {
        commandText.text = "> " + currentCommand; // Обновляем текст в текстовом поле
    }

    // Метод для выполнения команды
    void ExecuteCommand()
    {
        // Считываем текущую команду
        string currentCommandText = currentCommand;

        // Очищаем текущую команду
        currentCommand = "";

        // Обновляем текст командной строки
        UpdateCommandLineText();

        // Если введена команда "go to the cube"
        if (currentCommandText == "go to the cube")
        {
            // Запускаем NavMeshAgent для движения к кубику
            agent.SetDestination(cubeToGo.transform.position);

            // Скрываем текст обратной связи
            HideFeedback();
        }
        else // Если введена неправильная команда
        {
            // Отображаем текст "unknown command"
            ShowFeedback("unknown command");
        }
    }

    // Метод для отображения текста обратной связи
    void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);
        feedbackDisplayTimer = feedbackDisplayDuration; // Устанавливаем таймер равным продолжительности отображения
    }

    // Метод для скрытия текста обратной связи
    void HideFeedback()
    {
        if (feedbackDisplayTimer > 0f) // Проверяем, что таймер еще не истек
        {
            feedbackDisplayTimer -= Time.deltaTime; // Уменьшаем таймер
            if (feedbackDisplayTimer <= 0f) // Если время истекло
            {
                feedbackText.gameObject.SetActive(false); // Скрываем текст обратной связи
            }
        }
    }
}
