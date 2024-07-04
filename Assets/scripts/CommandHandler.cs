using UnityEngine;
using UnityEngine.UI;

public class CommandHandler : MonoBehaviour
{
    public Text commandText;
    public Text feedbackText;

    private string currentCommand = "";
    private bool commandEntered = false;
    private float feedbackDisplayDuration = 1f; // Время отображения обратной связи в секундах
    private float feedbackDisplayTimer = 0f;

    private InventoryManager inventoryManager;
    private ShopManager shopManager;
    private MovementController movementController;

    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
        shopManager = GetComponent<ShopManager>();
        movementController = GetComponent<MovementController>();

        UpdateCommandLineText();
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
        commandText.text = "> " + currentCommand;
    }

    void ExecuteCommand()
    {
        string command = currentCommand.ToLower().Trim();
        currentCommand = "";
        UpdateCommandLineText();

        if (command.StartsWith("go to the"))
        {
            movementController.HandleGoCommand(command);
        }
        else if (command.StartsWith("take "))
        {
            inventoryManager.HandleTakeCommand(command);
        }
        else if (command == "recycle junk")
        {
            inventoryManager.HandleRecycleCommand();
        }
        else if (command == "open shop")
        {
            shopManager.HandleOpenShopCommand();
        }
        else if (command == "close shop")
        {
            shopManager.HandleCloseShopCommand();
        }
        else
        {
            ShowFeedback("Unknown command");
        }
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
