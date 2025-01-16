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
    private int chosenCardIndex = 0;  // Индекс выбранного самолета

    public const int airplanePrice = 1400; // Цена каждого самолета
    [SerializeField] private Image _planeImage;
    [SerializeField] private Sprite[] _spritesPlanes;

    private void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        //totalCoins = 5000;
        UpdateCoinsUI();

        // Загружаем индекс выбранного самолета
        chosenCardIndex = PlayerPrefs.GetInt("ChosenAirplane", 0);

        // Устанавливаем начальное состояние карточек
        InitializeCards();

        // Привязываем методы к стрелкам
        leftArrow.onClick.AddListener(OnLeftArrowClick);
        rightArrow.onClick.AddListener(OnRightArrowClick);

        // Устанавливаем начальное отображение карточек
        UpdateCardDisplay();

        _planeImage.sprite = _spritesPlanes[chosenCardIndex];
    }

    private void InitializeCards()
    {
        for (int i = 0; i < airplaneCards.Length; i++)
        {
            var cardScript = airplaneCards[i].GetComponent<AirplaneCard>();

            if (i == 0 && PlayerPrefs.GetInt($"Airplane_{i}_Purchased", 0) == 0)
            {
                // Первый самолет всегда куплен и выбран, если его состояние не сохранено
                PlayerPrefs.SetInt($"Airplane_{i}_Purchased", 1);
            }

            if (i == chosenCardIndex)
            {
                // Если самолет выбран
                cardScript.SetState(false, false, true); // Выбран
            }
            else if (PlayerPrefs.GetInt($"Airplane_{i}_Purchased", 0) == 1)
            {
                // Если самолет куплен, но не выбран
                cardScript.SetState(false, true, false);
            }
            else
            {
                // Если самолет не куплен
                cardScript.SetState(true, false, false);
            }
        }
    }

    public void UpdateCoinsUI()
    {
        coinsText.text = totalCoins.ToString();
    }

    public void OnLeftArrowClick()
    {
        currentCardIndex = (currentCardIndex == 0) ? airplaneCards.Length - 1 : currentCardIndex - 1;
        UpdateCardDisplay();
    }

    public void OnRightArrowClick()
    {
        currentCardIndex = (currentCardIndex == airplaneCards.Length - 1) ? 0 : currentCardIndex + 1;
        UpdateCardDisplay();
    }

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

    public void SaveChosenAirplane(int cardIndex)
    {
        chosenCardIndex = cardIndex;
        PlayerPrefs.SetInt("ChosenAirplane", chosenCardIndex);
        PlayerPrefs.Save();
        _planeImage.sprite = _spritesPlanes[cardIndex];
    }
}
