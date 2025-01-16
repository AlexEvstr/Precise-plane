using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Text coinsText;          // Текст для отображения монет
    public Button leftArrow;        // Кнопка "Левая стрелка"
    public Button rightArrow;       // Кнопка "Правая стрелка"
    public GameObject[] airplaneCards; // Массив карточек самолетов

    public int totalCoins;          // Количество монет игрока
    private int currentCardIndex = 0; // Индекс текущей карточки
    public const int airplanePrice = 1400;

    private void Start()
    {
        // Загружаем монеты из PlayerPrefs
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinsUI();

        // Устанавливаем начальное состояние карточек
        InitializeCards();

        // Привязываем методы к стрелкам
        leftArrow.onClick.AddListener(OnLeftArrowClick);
        rightArrow.onClick.AddListener(OnRightArrowClick);

        // Устанавливаем начальное отображение карточек
        UpdateCardDisplay();
    }

    private void InitializeCards()
    {
        for (int i = 0; i < airplaneCards.Length; i++)
        {
            var cardScript = airplaneCards[i].GetComponent<AirplaneCard>();

            if (i == 0)
            {
                // Первый самолет всегда куплен и выбран
                PlayerPrefs.SetInt($"Airplane_{i}_Purchased", 1);
                cardScript.SetState(false, false, true); // Выбран
            }
            else
            {
                // Остальные самолеты не куплены
                if (PlayerPrefs.GetInt($"Airplane_{i}_Purchased", 0) == 1)
                {
                    cardScript.SetState(false, true, false); // Куплен, но не выбран
                }
                else
                {
                    cardScript.SetState(true, false, false); // Не куплен
                }
            }
        }
    }

    public void UpdateCoinsUI()
    {
        coinsText.text = totalCoins.ToString();
    }

    // Метод для левой стрелки
    public void OnLeftArrowClick()
    {
        currentCardIndex = (currentCardIndex == 0) ? airplaneCards.Length - 1 : currentCardIndex - 1;
        UpdateCardDisplay();
    }

    // Метод для правой стрелки
    public void OnRightArrowClick()
    {
        currentCardIndex = (currentCardIndex == airplaneCards.Length - 1) ? 0 : currentCardIndex + 1;
        UpdateCardDisplay();
    }

    // Обновление отображения карточек
    private void UpdateCardDisplay()
    {
        for (int i = 0; i < airplaneCards.Length; i++)
        {
            airplaneCards[i].SetActive(i == currentCardIndex);
        }
    }

    public void SetAllCardsToChooseState()
    {
        foreach (var card in airplaneCards)
        {
            var cardScript = card.GetComponent<AirplaneCard>();
            if (PlayerPrefs.GetInt($"Airplane_{cardScript.cardIndex}_Purchased", 0) == 1)
            {
                cardScript.SetState(false, true, false); // Куплен, но не выбран
            }
        }
    }
}
