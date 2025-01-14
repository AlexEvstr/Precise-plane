using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image blackScreenImage; // Ссылка на Image
    public float fadeDuration = 1.0f; // Длительность затемнения

    private void Start()
    {
        // Убедимся, что экран начинается прозрачным
        blackScreenImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeIn());
    }

    // Метод для переключения сцены
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreenImage.color = new Color(0, 0, 0, 1 - (timer / fadeDuration));
            yield return null;
        }
        blackScreenImage.color = new Color(0, 0, 0, 0); // Убедимся, что экран полностью прозрачный
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            blackScreenImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        blackScreenImage.color = new Color(0, 0, 0, 1); // Убедимся, что экран полностью черный
        SceneManager.LoadScene(sceneName); // Загрузка новой сцены
    }
}
