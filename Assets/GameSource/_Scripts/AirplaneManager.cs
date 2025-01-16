using UnityEngine;

public class AirplaneManager : MonoBehaviour
{
    public GameObject airplane;        // Ссылка на объект самолета
    private float horizontalSpeed = 1f; // Базовая скорость движения влево-вправо
    private float verticalSpeed = 3f;   // Базовая скорость движения вверх
    public Vector2 resetPosition = new Vector2(0, -3); // Позиция для сброса самолета

    private bool moveHorizontally = true; // Флаг для контроля типа движения
    private float targetX;               // Текущая цель по X
    private float leftBoundary;          // Левая граница
    private float rightBoundary;         // Правая граница
    [SerializeField] private Sprite[] _planeSprites;

    private void Start()
    {
        // Устанавливаем спрайт самолета в зависимости от выбранного
        airplane.GetComponent<SpriteRenderer>().sprite = _planeSprites[PlayerPrefs.GetInt("ChosenAirplane", 0)];

        // Загружаем уровень прокачки скоростей самолета
        int horizontalSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_0", 1);
        int verticalSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_1", 1);

        // Рассчитываем скорости с учетом прокачки
        horizontalSpeed += horizontalSpeedLevel * 0.1f;
        verticalSpeed += verticalSpeedLevel * 0.1f;

        // Вычисляем границы экрана
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        float airplaneWidth = airplane.GetComponent<SpriteRenderer>().bounds.extents.x;

        leftBoundary = -screenBounds.x + airplaneWidth;
        rightBoundary = screenBounds.x - airplaneWidth;

        // Устанавливаем начальную цель
        targetX = rightBoundary;
    }

    private void Update()
    {
        if (airplane == null) return;

        if (moveHorizontally)
        {
            MoveHorizontally();
        }
        else
        {
            MoveVertically();
        }
    }

    private void MoveHorizontally()
    {
        // Линейно перемещаем самолет к текущей цели
        airplane.transform.position = Vector3.MoveTowards(
            airplane.transform.position,
            new Vector3(targetX, airplane.transform.position.y, airplane.transform.position.z),
            horizontalSpeed * Time.deltaTime
        );

        // Проверяем, достигнута ли цель
        if (Mathf.Abs(airplane.transform.position.x - targetX) < 0.01f)
        {
            // Меняем цель на противоположную
            targetX = targetX == rightBoundary ? leftBoundary : rightBoundary;
        }
    }

    private void MoveVertically()
    {
        airplane.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }

    public void SwitchToVerticalMovement()
    {
        moveHorizontally = false; // Переключаем на движение вверх
    }

    public void OnAirplaneCollision(string tag)
    {
        if (tag == "target" || tag == "border")
        {
            // Сбрасываем самолет в исходное положение
            airplane.transform.position = resetPosition;

            // Возобновляем движение влево-вправо
            moveHorizontally = true;

            // Сбрасываем цель движения
            targetX = rightBoundary;
        }
    }
}
