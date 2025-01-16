using UnityEngine;
using UnityEngine.UI;

public class AirplaneSkills : MonoBehaviour
{
    [Header("Skill Settings")]
    public int cardIndex;                     // Индекс карточки самолета
    public Button[] upgradeButtons;           // Кнопки для прокачки (по одной на скилл)
    public GameObject[] skillChargesObjects;  // Объекты, содержащие массив зарядов для каждого скилла
    public int[] skillLevels = { 1, 1, 1 };   // Текущие уровни скиллов (по умолчанию)
    public int[] skillCosts = { 75, 100, 125 }; // Стоимость прокачки для каждого скилла

    private const int maxSkillLevel = 10;     // Максимальный уровень скилла
    private ShopManager shopManager;
    [SerializeField] private SettingsManager _settingsManager;

    private void Start()
    {
        // Получаем ссылку на ShopManager
        shopManager = FindObjectOfType<ShopManager>();

        // Инициализация скиллов для текущего самолета
        LoadSkillLevels();

        // Проверяем состояние кнопок
        UpdateUpgradeButtons();

        // Привязываем методы прокачки к кнопкам
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int skillIndex = i; // Локальная копия индекса для корректной передачи в лямбду
            upgradeButtons[i].onClick.AddListener(() => UpgradeSkill(skillIndex));
        }
    }

    private void LoadSkillLevels()
    {
        // Загружаем уровни скиллов из PlayerPrefs для конкретного самолета
        for (int i = 0; i < skillLevels.Length; i++)
        {
            string skillKey = $"Airplane_{cardIndex}_Skill_{i}";
            skillLevels[i] = PlayerPrefs.GetInt(skillKey, skillLevels[i]);
            UpdateSkillCharges(i);
        }
    }

    private void SaveSkillLevel(int skillIndex)
    {
        // Сохраняем уровень скилла в PlayerPrefs
        string skillKey = $"Airplane_{cardIndex}_Skill_{skillIndex}";
        PlayerPrefs.SetInt(skillKey, skillLevels[skillIndex]);
        PlayerPrefs.Save();
    }

    private void UpgradeSkill(int skillIndex)
    {
        // Проверяем, куплен ли самолет
        if (PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 0)
        {
            Debug.Log($"Cannot upgrade skill. Airplane {cardIndex} is not purchased.");
            return;
        }

        // Проверяем, достигнут ли максимальный уровень
        if (skillLevels[skillIndex] >= maxSkillLevel)
        {
            Debug.Log($"Skill {skillIndex} is already at max level.");
            return;
        }

        // Проверяем, достаточно ли монет
        if (shopManager.totalCoins < skillCosts[skillIndex])
        {
            Debug.Log($"Not enough coins to upgrade skill {skillIndex}.");
            _settingsManager.PlayDeclibeSound();
            return;
        }

        // Списываем монеты и обновляем UI
        shopManager.totalCoins -= skillCosts[skillIndex];
        shopManager.UpdateCoinsUI();
        _settingsManager.PlayCashSound();
        // Увеличиваем уровень скилла
        skillLevels[skillIndex]++;
        SaveSkillLevel(skillIndex);
        UpdateSkillCharges(skillIndex);

        Debug.Log($"Upgraded skill {skillIndex} to level {skillLevels[skillIndex]}.");
    }

    private void UpdateSkillCharges(int skillIndex)
    {
        // Обновляем визуальное отображение зарядов скилла
        Transform charges = skillChargesObjects[skillIndex].transform;
        for (int i = 0; i < charges.childCount; i++)
        {
            charges.GetChild(i).gameObject.SetActive(i < skillLevels[skillIndex]);
        }
    }

    public void UpdateUpgradeButtons()
    {
        // Проверяем, куплен ли самолет
        bool isPurchased = PlayerPrefs.GetInt($"Airplane_{cardIndex}_Purchased", 0) == 1;

        // Разблокируем кнопки, если самолет куплен
        foreach (var button in upgradeButtons)
        {
            button.interactable = isPurchased;
        }
    }
}
