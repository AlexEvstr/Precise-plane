using UnityEngine;

public class TargetMover : MonoBehaviour
{
    private float speed = 2f;       // Базовая скорость движения
    public float xMin = -2.25f;    // Левая граница движения
    public float xMax = 2.25f;     // Правая граница движения

    private float targetX;         // Целевая позиция по X

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

        // Устанавливаем начальную цель движения
        targetX = xMax;
    }

    private void Update()
    {
        // Линейно перемещаем цель к текущей целевой позиции
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(targetX, transform.position.y, transform.position.z),
            speed * Time.deltaTime
        );

        // Проверяем, достигнута ли целевая позиция
        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            // Меняем цель на противоположную
            targetX = targetX == xMax ? xMin : xMax;
        }
    }
}
