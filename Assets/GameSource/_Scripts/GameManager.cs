using System;
using System.Collections;
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
    [SerializeField] private Text _coinsToAdd;
    [SerializeField] private GameObject _coinImage;
    private int _totalHitsCount;
    private int _totalGamesCount;

    private GameWindowManager _gameWindowManager;

    private void Start()
    {
        _totalHitsCount = PlayerPrefs.GetInt("TotalHits", 0);
        _totalGamesCount = PlayerPrefs.GetInt("TotalGames", 0);
        _gameWindowManager = GetComponent<GameWindowManager>();
        // Загружаем общее количество монет из PlayerPrefs
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinUI();
    }

    // Начисление монет
    public void AddCoins()
    {
        IncreaseAndHits();
        int coinsToAdd = UnityEngine.Random.Range(50, 76); // Рандомное значение от 50 до 75 с шагом 5
        coinsToAdd -= coinsToAdd % 5;          // Гарантируем кратность 5
        //_coinsToAdd.text = $"+{coinsToAdd}";
        StartCoroutine(ShowCoinsText(coinsToAdd));

        totalCoins += coinsToAdd;
        roundCoins += coinsToAdd;
        
        // Сохраняем общее количество монет в PlayerPrefs
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        // Обновляем текстовые поля
        UpdateCoinUI();
    }

    private void IncreaseAndHits()
    {
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        
        _totalHitsCount++;
        PlayerPrefs.SetInt("TotalHits", _totalHitsCount);
        if (_totalHitsCount >= 100)
        {
            PlayerPrefs.SetInt("Achieve_3", 1);
            PlayerPrefs.SetString("DateForAchieve_2", currentDate);
        }
        else if (_totalHitsCount >= 50)
        {
            PlayerPrefs.SetInt("Achieve_2", 1);
            PlayerPrefs.SetString("DateForAchieve_1", currentDate);
        }
        else if (_totalHitsCount >= 10)
        {
            PlayerPrefs.SetInt("Achieve_1", 1);
            PlayerPrefs.SetString("DateForAchieve_0", currentDate);
        }
    }

    private IEnumerator ShowCoinsText(int coinsCount)
    {
        _coinImage.SetActive(true);
        _coinsToAdd.text = $"+{coinsCount}";
        yield return new WaitForSeconds(1.0f);
        _coinImage.SetActive(false);
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
            _gameWindowManager.OpenGameOver();
            IncreaseAndCheckGames();
        }
    }

    private void IncreaseAndCheckGames()
    {
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

        _totalGamesCount++;
        PlayerPrefs.SetInt("TotalGames", _totalGamesCount);
        if (_totalGamesCount >= 100)
        {
            PlayerPrefs.SetInt("Achieve_6", 1);
            PlayerPrefs.SetString("DateForAchieve_5", currentDate);
        }
        else if (_totalGamesCount >= 50)
        {
            PlayerPrefs.SetInt("Achieve_5", 1);
            PlayerPrefs.SetString("DateForAchieve_4", currentDate);
        }
        else if (_totalGamesCount >= 10)
        {
            PlayerPrefs.SetInt("Achieve_4", 1);
            PlayerPrefs.SetString("DateForAchieve_3", currentDate);
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
