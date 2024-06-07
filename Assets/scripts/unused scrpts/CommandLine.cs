using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CommandLine : MonoBehaviour
{
    public Text commandText;
    public Text feedbackText;
    public NavMeshAgent agent;
    public GameObject cubeToGo;
    public GameObject ballToGo;
    public GameObject ballItemToTake;
    public GameObject cubeItemToTake;
    public GameObject yellowBoxItemToTake;
    public GameObject yellowBox;
    public Animator doorAnimator;
    public Image ballIcon;
    public Image cubeIcon;
    public Image yellowBoxIcon;
    public GameObject door;
    public GameObject hatToGo;
    public GameObject groundHat;
    public GameObject characterHat;

    // Переменные для хлама и монет
    public GameObject junkToGo;
    public GameObject junkItemToTake;
    public GameObject recyclingMachine;
    public Text junkCountText;
    public Text coinCountText;

    // Переменная для магазина
    public GameObject shop;
    public Image shopInventoryImage;

    // Кнопки для покупки скиллпоинтов
    public Button buySkillPointButton1;
    public Button buySkillPointButton2;

    private string currentCommand = "";
    private bool commandEntered = false;
    private float feedbackDisplayDuration = 2f;
    private float feedbackDisplayTimer = 0f;
    private bool ballTaken = false;
    private bool cubeTaken = false;
    private bool boxTaken = false;
    private bool doorOpened = false;
    private bool hatTaken = false;

    private int junkCount = 0;
    private int coinCount = 0;

    void Start()
    {
        UpdateCommandLineText();
        UpdateJunkCountText();
        UpdateCoinCountText();
        shopInventoryImage.gameObject.SetActive(false); // Скрыть изображение инвентаря по умолчанию

        // Привязываем методы к кнопкам покупки скиллпоинтов
        buySkillPointButton1.onClick.AddListener(BuySkillPoint1);
        buySkillPointButton2.onClick.AddListener(BuySkillPoint2);
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
        HideFeedback();
        commandEntered = false;
    }

    void UpdateCommandLineText()
    {
        commandText.text = "> " + currentCommand;
    }

    void UpdateJunkCountText()
    {
        junkCountText.text = "Junk: " + junkCount;
    }

    void UpdateCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount;
    }

    void ExecuteCommand()
    {
        string command = currentCommand.ToLower().Trim();
        currentCommand = "";
        UpdateCommandLineText();

        if (command.StartsWith("go to the "))
        {
            HandleGoCommand(command);
        }
        else if (command.StartsWith("take "))
        {
            HandleTakeCommand(command);
        }
        else if (command == "recycle junk")
        {
            HandleRecycleCommand();
        }
        else if (command == "open shop")
        {
            HandleOpenShopCommand();
        }
        else if (command == "close shop")
        {
            HandleCloseShopCommand();
        }
        else
        {
            ShowFeedback("Unknown command");
        }
    }

    void HandleGoCommand(string command)
    {
        if (command == "go to the cube")
        {
            MoveAgentTo(cubeToGo);
        }
        else if (command == "go to the hat")
        {
            MoveAgentTo(hatToGo);
        }
        else if (command == "go to the ball")
        {
            MoveAgentTo(ballToGo);
        }
        else if (command == "go to the box")
        {
            MoveAgentTo(yellowBox);
        }
        else if (command == "go to the door")
        {
            MoveAgentTo(door);
        }
        else if (command == "go to the junk")
        {
            MoveAgentTo(junkToGo);
        }
        else if (command == "go to the machine")
        {
            MoveAgentTo(recyclingMachine);
        }
        else if (command == "go to the shop")
        {
            MoveAgentTo(shop);
        }
        else
        {
            ShowFeedback("Unknown command");
        }
    }

    void HandleTakeCommand(string command)
    {
        if (command == "take hat")
        {
            TakeItem(hatToGo, characterHat, ref hatTaken);
        }
        else if (command == "take ball")
        {
            TakeItem(ballItemToTake, ballIcon.gameObject, ref ballTaken);
        }
        else if (command == "take cube")
        {
            TakeItem(cubeItemToTake, cubeIcon.gameObject, ref cubeTaken);
        }
        else if (command == "take box")
        {
            TakeItem(yellowBoxItemToTake, yellowBoxIcon.gameObject, ref boxTaken);
        }
        else if (command == "take junk")
        {
            if (junkItemToTake != null)
            {
                Destroy(junkItemToTake);
                junkCount++;
                UpdateJunkCountText();
                HideFeedback();
            }
            else
            {
                ShowFeedback("Item not found");
            }
        }
        else
        {
            ShowFeedback("Unknown command");
        }

        CheckItemsTaken();
    }

    void HandleRecycleCommand()
    {
        if (junkCount > 0)
        {
            coinCount += junkCount * 140; // 1 хлам = 140 монет
            junkCount = 0;
            UpdateCoinCountText();
            UpdateJunkCountText();
            ShowFeedback("Recycled junk and earned coins!");
        }
        else
        {
            ShowFeedback("No junk to recycle.");
        }
    }

    void HandleOpenShopCommand()
    {
        shopInventoryImage.gameObject.SetActive(true); // Показать изображение инвентаря
        ShowFeedback("Opened the shop.");
    }

    void HandleCloseShopCommand()
    {
        shopInventoryImage.gameObject.SetActive(false); // Скрыть изображение инвентаря
        ShowFeedback("Closed the shop.");
    }

    void MoveAgentTo(GameObject target)
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            HideFeedback();
        }
        else
        {
            ShowFeedback("Target not found");
        }
    }

    void TakeItem(GameObject item, GameObject icon, ref bool takenFlag)
    {
        if (item != null)
        {
            Destroy(item);
            if (icon != null)
            {
                icon.SetActive(true);
            }
            takenFlag = true;
            HideFeedback();
        }
        else
        {
            ShowFeedback("Item not found");
        }
    }

    void ShowFeedback(string message)
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

    void CheckItemsTaken()
    {
        if (ballTaken && cubeTaken && boxTaken && !doorOpened && hatTaken)
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("OpenDoor");
                doorOpened = true;
            }
            else
            {
                Debug.LogError("Door Animator is not assigned!");
            }
        }
    }

    void BuySkillPoint1()
    {
        if (coinCount >= 60)
        {
            coinCount -= 60;
            UpdateCoinCountText();
            ShowFeedback("Bought skill point for 60 coins!");
        }
        else
        {
            ShowFeedback("Not enough coins to buy skill point.");
        }
    }

    void BuySkillPoint2()
    {
        if (coinCount >= 80)
        {
            coinCount -= 80;
            UpdateCoinCountText();
            ShowFeedback("Bought skill point for 80 coins!");
        }
        else
        {
            ShowFeedback("Not enough coins to buy skill point.");
        }
    }
}
