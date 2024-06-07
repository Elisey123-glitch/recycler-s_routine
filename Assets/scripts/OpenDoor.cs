using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator; // ������ �� �������� �����

    private bool allItemsTaken = false; // ����, �����������, ��� ��� �������� ���������

    // ����� ��� ��������, ��������� �� ��� ��������
    void CheckItemsTaken()
    {
        if (allItemsTaken)
        {
            // ���� ��� �������� ���������, ������������� ������� "OpenDoor" � true
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
}
