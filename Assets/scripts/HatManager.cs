using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject characterHat; // ����� �� ���������

    // ����� ��� ������� ����� � �����
    public void TakeHat()
    {
        characterHat.SetActive(true); // ���������� ����� �� ���������
    }
}
