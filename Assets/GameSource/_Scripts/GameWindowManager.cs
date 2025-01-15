using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindowManager : MonoBehaviour
{
    public float animationDuration = 0.5f; // Длительность анимации
    [SerializeField] private GameObject _gameOverWindow;

    public void OpenGameOver()
    {
        StartCoroutine(ExpandWindow());
    }

    private IEnumerator ExpandWindow(float duration = 0.5f)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 startScale = Vector3.zero; // Начальный размер (0, 0, 0)
        Vector3 endScale = Vector3.one;   // Конечный размер (1, 1, 1)
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            _gameOverWindow.transform.localScale = Vector3.Lerp(startScale, endScale, progress); // Плавное изменение размера
            yield return null;
        }

        _gameOverWindow.transform.localScale = endScale; // Убедимся, что окно достигло конечного размера
    }
}
