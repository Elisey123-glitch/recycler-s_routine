using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public string sceneToLoad; // ��� ����� ��� ��������
    public Image loadingImage; // ����������� ��� ����������� ��������� ��������
    public float fillSpeed = 0.5f; // �������� ����������

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // ������ ����������� �������� �����
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (loadingImage.fillAmount < 1f)
        {
            // �������� �������� �� 0 �� 0.9
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // ������� ���������� ��������
            loadingImage.fillAmount = Mathf.MoveTowards(loadingImage.fillAmount, targetProgress, fillSpeed * Time.deltaTime);

            // ���� ���������� �����
            yield return null;
        }

        // ��������� ��������� ����� ����� ������� ����������
        operation.allowSceneActivation = true;
    }
}
