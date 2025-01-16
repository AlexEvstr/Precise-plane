using UnityEngine;

public class TargetMover : MonoBehaviour
{
    private float speed = 2f;       // Базовая скорость движения
    public float xMin = -2.25f;    // Левая граница движения
    public float xMax = 2.25f;     // Правая граница движения

    private int direction = 1;    // Направление движения (1 = вправо, -1 = влево)

    private void Start()
    {
        // Загружаем уровень прокачки скорости цели
        int targetSpeedLevel = PlayerPrefs.GetInt($"Airplane_{PlayerPrefs.GetInt("ChosenAirplane", 0)}_Skill_2", 1);

        // Рассчитываем скорость с учетом прокачки (уменьшаем скорость на 0.1 за уровень)
        speed -= targetSpeedLevel * 0.1f;

        // Убедимся, что скорость не стала отрицательной
        if (speed < 0.1f)
        {
            speed = 0.1f;
        }
    }

    private void Update()
    {
        // Двигаем цель в текущем направлении
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        // Проверяем границы и меняем направление
        if (transform.position.x > xMax || transform.position.x < xMin)
        {
            direction *= -1; // Меняем направление
        }
    }
}
