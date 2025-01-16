using UnityEngine;

public class AirplaneManager : MonoBehaviour
{
    public GameObject airplane;        // Ссылка на объект самолета
    public float horizontalSpeed = 2f; // Скорость движения влево-вправо
    public float verticalSpeed = 3f;   // Скорость движения вверх
    public Vector2 resetPosition = new Vector2(0, -3); // Позиция для сброса самолета

    private bool moveHorizontally = true; // Флаг для контроля типа движения
    private Vector2 screenBounds;        // Границы экрана
    private float airplaneWidth;         // Половина ширины самолета
    [SerializeField] private Sprite[] _planeSprites;

    private void Start()
    {
        airplane.GetComponent<SpriteRenderer>().sprite = _planeSprites[PlayerPrefs.GetInt("ChosenAirplane", 0)];

        // Вычисляем границы экрана
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        airplaneWidth = airplane.GetComponent<SpriteRenderer>().bounds.extents.x;
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

    // Движение влево-вправо
    private void MoveHorizontally()
    {
        airplane.transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;

        // Проверяем границы экрана
        if (airplane.transform.position.x > screenBounds.x - airplaneWidth || airplane.transform.position.x < -screenBounds.x + airplaneWidth)
        {
            horizontalSpeed *= -1; // Меняем направление
        }
    }

    // Движение вверх
    private void MoveVertically()
    {
        airplane.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }

    // Публичный метод для переключения движения
    public void SwitchToVerticalMovement()
    {
        moveHorizontally = false; // Переключаем на движение вверх
    }

    // Обработка столкновений самолета
    public void OnAirplaneCollision(string tag)
    {
        if (tag == "target" || tag == "border")
        {
            // Сбрасываем самолет в исходное положение
            airplane.transform.position = resetPosition;

            // Возобновляем движение влево-вправо
            moveHorizontally = true;
        }
    }
}
