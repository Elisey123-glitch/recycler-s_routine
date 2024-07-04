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
        // Устанавливаем анимацию idle при старте сцены
        if (animator != null)
        {
            animator.SetTrigger("Idle");
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

        // Поддержка анимации idle
        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Idle");
        }
    }

    void LookAtPlayer()
    {
        // Вычисляем направление к игроку
        Vector3 direction = player.position - transform.position;

        // Поворачиваем только по оси Y, игнорируя компоненты по осям X и Z
        direction.y = 0; // Оставляем только горизонтальное направление

        // Создаем целевое вращение
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Устанавливаем новое вращение, сохраняя текущие углы по осям X и Z
        Vector3 newRotation = new Vector3(transform.eulerAngles.x, targetRotation.eulerAngles.y, transform.eulerAngles.z);

        // Применяем новое вращение
        transform.rotation = Quaternion.Euler(newRotation);
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
