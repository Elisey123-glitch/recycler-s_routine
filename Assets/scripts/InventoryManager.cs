using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Text junkCountText;
    public Text coinCountText;

    public GameObject junkItemToTake;

    private int junkCount = 0;
    private int coinCount = 0;
    private CommandHandler commandHandler;

    void Start()
    {
        commandHandler = GetComponent<CommandHandler>();
        UpdateJunkCountText();
        UpdateCoinCountText();
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
}
