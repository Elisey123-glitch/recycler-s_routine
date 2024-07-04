using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextReveal : MonoBehaviour
{
    public Text uiText; // ��������� ����
    public float revealSpeed = 0.05f; // �������� ��������� ������
    public GameObject commandWindow; // ���� ��������� ������

    void Start()
    {
        commandWindow.SetActive(false); // �������� ���� ��������� ������ � ������
        StartCoroutine(RevealText(uiText.text));
    }

    IEnumerator RevealText(string fullText)
    {
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(revealSpeed);
        }
        ShowCommandWindow(); // ���������� ���� ��������� ������ ����� ���������� ��������
    }

    void ShowCommandWindow()
    {
        commandWindow.SetActive(true);
    }

    // ����� ��� �������������� ����������� ������, ������� ����� ������� �����
    public void RevealNewText(string newText)
    {
        StartCoroutine(RevealText(newText));
    }
}
