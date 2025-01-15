using System.Collections;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GameObject menuWindow;       // Главное меню
    public GameObject settingsWindow;   // Окно настроек
    public GameObject shopWindow;       // Окно магазина
    public GameObject achievementsWindow; // Окно ачивок
    public GameObject _saveBtn;
    public GameObject _backBtn;

    public float animationDuration = 0.5f; // Длительность анимации

    // Метод для открытия указанного окна
    public void OpenWindow(GameObject windowToOpen)
    {
        StartCoroutine(AnimateWindow(windowToOpen, true));
        menuWindow.SetActive(false); // Скрываем меню
    }

    // Метод для закрытия указанного окна и возврата в главное меню
    public void CloseWindowAndOpenMenu(GameObject windowToClose)
    {
        StartCoroutine(AnimateWindow(windowToClose, false, () =>
        {
            menuWindow.SetActive(true); // Открываем меню после анимации
        }));
        _saveBtn.SetActive(false);
        _backBtn.SetActive(true);
    }

    // Анимация открытия или закрытия окна
    private IEnumerator AnimateWindow(GameObject window, bool isOpening, System.Action onComplete = null)
    {
        window.SetActive(true); // Убедимся, что окно включено
        Transform windowTransform = window.transform;
        Vector3 startScale = isOpening ? Vector3.zero : Vector3.one; // Начальный масштаб
        Vector3 endScale = isOpening ? Vector3.one : Vector3.zero;   // Конечный масштаб

        float timer = 0f;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            windowTransform.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }

        windowTransform.localScale = endScale; // Убедимся, что достигли конечного значения

        if (!isOpening)
        {
            window.SetActive(false); // Отключаем окно после закрытия
        }

        onComplete?.Invoke(); // Вызываем callback после завершения анимации
    }
}
