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
        // ������������� �������� idle ��� ������ �����
        if (animator != null)
        {
            animator.SetTrigger("Idle");
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

        // ��������� �������� idle
        if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Idle");
        }
    }

    void LookAtPlayer()
    {
        // ��������� ����������� � ������
        Vector3 direction = player.position - transform.position;

        // ������������ ������ �� ��� Y, ��������� ���������� �� ���� X � Z
        direction.y = 0; // ��������� ������ �������������� �����������

        // ������� ������� ��������
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ������������� ����� ��������, �������� ������� ���� �� ���� X � Z
        Vector3 newRotation = new Vector3(transform.eulerAngles.x, targetRotation.eulerAngles.y, transform.eulerAngles.z);

        // ��������� ����� ��������
        transform.rotation = Quaternion.Euler(newRotation);
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
