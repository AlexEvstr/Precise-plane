using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text totalCoinsText;        // Текст для отображения общего количества монет
    public Text roundCoinsText;        // Текст для отображения монет в текущем раунде
    public GameObject[] lifeIcons;     // Иконки жизней

    private int totalCoins;            // Общее количество монет
    private int roundCoins;            // Монеты за текущий раунд
    private int lives = 3;             // Количество жизней

    private void Start()
    {
        // Загружаем общее количество монет из PlayerPrefs
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinUI();
    }

    // Начисление монет
    public void AddCoins()
    {
        int coinsToAdd = Random.Range(50, 76); // Рандомное значение от 50 до 75 с шагом 5
        coinsToAdd -= coinsToAdd % 5;          // Гарантируем кратность 5

        totalCoins += coinsToAdd;
        roundCoins += coinsToAdd;

        // Сохраняем общее количество монет в PlayerPrefs
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        // Обновляем текстовые поля
        UpdateCoinUI();
    }

    // Обновление UI для монет
    private void UpdateCoinUI()
    {
        totalCoinsText.text = totalCoins.ToString();
        roundCoinsText.text = roundCoins.ToString();
    }

    // Обработка потери жизни
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            lifeIcons[lives].SetActive(false); // Выключаем иконку жизни
        }

        if (lives == 0)
        {
            Debug.Log("Game Over");
            // Здесь можно добавить вызов метода для отображения окна Game Over
        }
    }

    // Сброс текущего раунда (при необходимости)
    public void ResetRound()
    {
        roundCoins = 0;
        UpdateCoinUI();
        lives = 3;

        // Включаем все иконки жизней
        foreach (var icon in lifeIcons)
        {
            icon.SetActive(true);
        }
    }
}
