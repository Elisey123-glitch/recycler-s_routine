using UnityEngine;

public class TeacherBotController : MonoBehaviour
{
    public Transform player; // �����
    public GameObject[] messageIcons; // ������ ������ ���������
    public Camera mainCamera; // ������ �� ������� ������

    private int currentIconIndex = 0;

    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // ��������� ������ �������������� �����������
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // ������� �� ��������
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
