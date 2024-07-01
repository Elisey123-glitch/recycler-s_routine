using UnityEngine;

public class TeacherBotController : MonoBehaviour
{
    public Transform player; // �����
    public GameObject[] messageIcons; // ������ ������ ���������
    public Camera mainCamera; // ������ �� ������� ������
    public Animator animator; // ������ �� ��������

    private int currentIconIndex = 0;
    private bool iconsVisible = true; // ���� ��� ����������� ������

    void Start()
    {
        // ������ �������� ��� ������ �����
        if (animator != null)
        {
            animator.SetTrigger("StartAnimation");
        }

        // �������� ������ ������
        if (messageIcons.Length > 0)
        {
            messageIcons[0].SetActive(true);
        }
    }

    void Update()
    {
        LookAtPlayer();

        // ������������ ������ �� ������� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextMessageIcon();
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // ��������� ������ �������������� �����������
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void ShowNextMessageIcon()
    {
        if (!iconsVisible || messageIcons.Length == 0) return;

        // ������ ������� ������
        messageIcons[currentIconIndex].SetActive(false);

        // ������������ �� ��������� ������
        currentIconIndex++;
        if (currentIconIndex < messageIcons.Length)
        {
            messageIcons[currentIconIndex].SetActive(true);
        }
        else
        {
            iconsVisible = false; // ��������� ������ ����� ���������
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
