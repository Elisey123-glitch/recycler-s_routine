using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Text junkCountText;
    public Text coinCountText;

    public GameObject junkItemToTake;

    public GameObject cubeToTake;
    public GameObject boxToTake;
    public GameObject ballToTake;
    public GameObject door;

    public Image cubeImage;
    public Image boxImage;
    public Image ballImage;

    private int junkCount = 0;
    private int coinCount = 0;
    private CommandHandler commandHandler;
    private bool hasCube = false;
    private bool hasBox = false;
    private bool hasBall = false;

    void Start()
    {
        commandHandler = GetComponent<CommandHandler>();
        UpdateJunkCountText();
        UpdateCoinCountText();
        HideAllImages(); // Ensure all images are hidden at start
    }

    public void HandleTakeCommand(string command)
    {
        if (command == "take junk")
        {
            if (junkItemToTake != null)
            {
                Destroy(junkItemToTake);
                junkCount++;
                UpdateJunkCountText();
            }
            else
            {
                commandHandler.ShowFeedback("Item not found");
            }
        }
        else if (command == "take cube")
        {
            if (cubeToTake != null)
            {
                Destroy(cubeToTake);
                ShowCubeImage();
                hasCube = true;
                CheckIfAllItemsCollected();
            }
            else
            {
                commandHandler.ShowFeedback("Cube not found");
            }
        }
        else if (command == "take box")
        {
            if (boxToTake != null)
            {
                Destroy(boxToTake);
                ShowBoxImage();
                hasBox = true;
                CheckIfAllItemsCollected();
            }
            else
            {
                commandHandler.ShowFeedback("Box not found");
            }
        }
        else if (command == "take ball")
        {
            if (ballToTake != null)
            {
                Destroy(ballToTake);
                ShowBallImage();
                hasBall = true;
                CheckIfAllItemsCollected();
            }
            else
            {
                commandHandler.ShowFeedback("Ball not found");
            }
        }
        else
        {
            commandHandler.ShowFeedback("Unknown take command");
        }
    }

    public void HandleRecycleCommand()
    {
        if (junkCount > 0)
        {
            coinCount += junkCount * 140; // 1 хлам = 140 монет
            junkCount = 0;
            UpdateCoinCountText();
            UpdateJunkCountText();
            commandHandler.ShowFeedback("Recycled junk and earned coins!");
        }
        else
        {
            commandHandler.ShowFeedback("No junk to recycle.");
        }
    }

    void ShowCubeImage()
    {
        cubeImage.gameObject.SetActive(true);
    }

    void ShowBoxImage()
    {
        boxImage.gameObject.SetActive(true);
    }

    void ShowBallImage()
    {
        ballImage.gameObject.SetActive(true);
    }

    void HideAllImages()
    {
        cubeImage.gameObject.SetActive(false);
        boxImage.gameObject.SetActive(false);
        ballImage.gameObject.SetActive(false);
    }

    void UpdateJunkCountText()
    {
        junkCountText.text = "Junk: " + junkCount;
    }

    void UpdateCoinCountText()
    {
        coinCountText.text = "Coins: " + coinCount;
    }

    public bool TrySpendCoins(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            UpdateCoinCountText();
            return true;
        }
        return false;
    }

    void CheckIfAllItemsCollected()
    {
        if (hasCube && hasBox && hasBall)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Animator doorAnimator = door.GetComponent<Animator>();
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            commandHandler.ShowFeedback("Door animator not found");
        }
    }
}
