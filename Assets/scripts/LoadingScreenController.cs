using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public string sceneToLoad; // Имя сцены для загрузки
    public Image loadingImage; // Изображение для отображения прогресса загрузки
    public float fillSpeed = 0.5f; // Скорость заполнения

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Начать асинхронную загрузку сцены
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (loadingImage.fillAmount < 1f)
        {
            // Прогресс загрузки от 0 до 0.9
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Плавное заполнение слайдера
            loadingImage.fillAmount = Mathf.MoveTowards(loadingImage.fillAmount, targetProgress, fillSpeed * Time.deltaTime);

            // Ждем следующего кадра
            yield return null;
        }

        // Разрешаем активацию сцены после полного заполнения
        operation.allowSceneActivation = true;
    }
}
