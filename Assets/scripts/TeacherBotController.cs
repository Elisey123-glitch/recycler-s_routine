using UnityEngine;

public class TeacherBotController : MonoBehaviour
{
    public Transform player; // Игрок
    public GameObject[] messageIcons; // Массив иконок сообщений
    public Camera mainCamera; // Ссылка на главную камеру

    private int currentIconIndex = 0;

    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Оставляем только горизонтальное направление
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Поворот на половину
        targetRotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void ShowNextMessageIcon()
    {
        if (messageIcons.Length == 0) return;

        HideAllIcons();
        messageIcons[currentIconIndex].SetActive(true);
        currentIconIndex = (currentIconIndex + 1) % messageIcons.Length;
    }

    public void HideAllIcons()
    {
        foreach (GameObject icon in messageIcons)
        {
            icon.SetActive(false);
        }
    }
}
