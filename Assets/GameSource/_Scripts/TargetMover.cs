using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float speed = 2f;       // Скорость движения
    public float xMin = -2.5f;    // Левая граница движения
    public float xMax = 2.5f;     // Правая граница движения

    private int direction = 1;    // Направление движения (1 = вправо, -1 = влево)

    private void Update()
    {
        // Двигаем цель в текущем направлении
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        // Проверяем границы и меняем направление
        if (transform.position.x >= xMax || transform.position.x <= xMin)
        {
            direction *= -1; // Меняем направление
        }
    }
}
