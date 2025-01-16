using UnityEngine;
using UnityEngine.UI;

public class AirplaneCard : MonoBehaviour
{
    public int cardIndex;            // Индекс карточки
    public Button buyOrChooseButton; // Кнопка покупки/выбора
    public GameObject priceObject;   // Дочерний объект кнопки: цена
    public GameObject chooseObject;  // Дочерний объект кнопки: "Choose"
    public GameObject chosenObject;  // Дочерний объект кнопки: "Chosen"

    private ShopManager shopManager; // Ссылка на ShopManager

    private void Start()
    {
        // Получаем ссылку на ShopManager
        shopManager = FindObjectOfType<ShopManager>();

        // Привязываем метод к кнопке
        buyOrChooseButton.onClick.AddListener(OnBuyOrChooseClick);
    }

    private void OnBuyOrChooseClick()
    {
        if (PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 1)
        {
            // Если самолет уже куплен, выбираем его
            shopManager.SetAllCardsToChooseState();
            shopManager.SaveChosenAirplane(cardIndex); // Сохраняем выбранный самолет
            SetState(false, false, true);             // Установить состояние "Chosen"
        }
        else
        {
            // Если самолет не куплен, проверяем монеты
            if (shopManager.totalCoins >= ShopManager.airplanePrice)
            {
                shopManager.totalCoins -= ShopManager.airplanePrice;
                shopManager.UpdateCoinsUI();

                // Сохраняем покупку
                PlayerPrefs.SetInt($"Airplane_{cardIndex}_Purchased", 1);
                PlayerPrefs.SetInt("TotalCoins", shopManager.totalCoins);
                PlayerPrefs.Save();

                // Выбираем самолет
                shopManager.SetAllCardsToChooseState();
                shopManager.SaveChosenAirplane(cardIndex); // Сохраняем выбранный самолет
                SetState(false, false, true);

                // Обновляем кнопки прокачки
                var airplaneSkills = GetComponent<AirplaneSkills>();
                if (airplaneSkills != null)
                {
                    airplaneSkills.UpdateUpgradeButtons();
                }
            }
        }
    }


    public void SetState(bool showPrice, bool showChoose, bool showChosen)
    {
        priceObject.SetActive(showPrice);
        chooseObject.SetActive(showChoose);
        chosenObject.SetActive(showChosen);
    }
}
