using UnityEngine;

public class TeacherBotController : MonoBehaviour
{
    public Transform player; // Игрок
    public GameObject[] messageIcons; // Массив иконок сообщений
    public Camera mainCamera; // Ссылка на главную камеру
    public Animator animator; // Ссылка на аниматор

    private int currentIconIndex = 0;
    private bool iconsVisible = true; // Флаг для отображения иконок

    void Start()
    {
        // Запуск анимации при старте сцены
        if (animator != null)
        {
            animator.SetTrigger("StartAnimation");
        }

        // Показать первую иконку
        if (messageIcons.Length > 0)
        {
            messageIcons[0].SetActive(true);
        }
    }

    void Update()
    {
        LookAtPlayer();

        // Переключение иконок по нажатию пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextMessageIcon();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Оставляем только горизонтальное направление
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void ShowNextMessageIcon()
    {
        if (!iconsVisible || messageIcons.Length == 0) return;

        // Скрыть текущую иконку
        messageIcons[currentIconIndex].SetActive(false);

        // Переключение на следующую иконку
        currentIconIndex++;
        if (currentIconIndex < messageIcons.Length)
        {
            messageIcons[currentIconIndex].SetActive(true);
        }
        else
        {
            iconsVisible = false; // Отключить иконки после последней
        }
    }

    public void HideAllIcons()
    {
        foreach (GameObject icon in messageIcons)
        {
            icon.SetActive(false);
        }
        iconsVisible = false;
    }
}
