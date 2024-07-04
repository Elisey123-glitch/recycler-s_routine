using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextReveal : MonoBehaviour
{
    public Text uiText; // Текстовое поле
    public float revealSpeed = 0.05f; // Скорость появления текста
    public GameObject commandWindow; // Окно командной строки

    void Start()
    {
        commandWindow.SetActive(false); // Скрываем окно командной строки в начале
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
        ShowCommandWindow(); // Показываем окно командной строки после завершения анимации
    }

    void ShowCommandWindow()
    {
        commandWindow.SetActive(true);
    }

    // Метод для анимированного отображения текста, который можно вызвать извне
    public void RevealNewText(string newText)
    {
        StartCoroutine(RevealText(newText));
    }
}
