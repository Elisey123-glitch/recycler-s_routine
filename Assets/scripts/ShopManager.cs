using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image shopInventoryImage;
    public Button buySkillPointButton1;
    public Button buySkillPointButton2;

    private InventoryManager inventoryManager;
    private CommandHandler commandHandler;

    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
        commandHandler = GetComponent<CommandHandler>();
        shopInventoryImage.gameObject.SetActive(false); // Скрыть изображение инвентаря по умолчанию

        // Привязываем методы к кнопкам покупки скиллпоинтов
        buySkillPointButton1.onClick.AddListener(BuySkillPoint1);
        buySkillPointButton2.onClick.AddListener(BuySkillPoint2);
    }

    public void HandleOpenShopCommand()
    {
        shopInventoryImage.gameObject.SetActive(true); // Показать изображение инвентаря
        commandHandler.ShowFeedback("Opened the shop.");
    }

    public void HandleCloseShopCommand()
    {
        shopInventoryImage.gameObject.SetActive(false); // Скрыть изображение инвентаря
        commandHandler.ShowFeedback("Closed the shop.");
    }

    void BuySkillPoint1()
    {
        if (inventoryManager.TrySpendCoins(60))
        {
            commandHandler.ShowFeedback("Bought skill point for 60 coins!");
        }
        else
        {
            commandHandler.ShowFeedback("Not enough coins to buy skill point.");
        }
    }

    void BuySkillPoint2()
    {
        if (inventoryManager.TrySpendCoins(80))
        {
            commandHandler.ShowFeedback("Bought skill point for 80 coins!");
        }
        else
        {
            commandHandler.ShowFeedback("Not enough coins to buy skill point.");
        }
    }
}
