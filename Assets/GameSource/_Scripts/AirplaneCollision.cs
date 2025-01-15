using UnityEngine;

public class AirplaneCollision : MonoBehaviour
{
    public AirplaneManager airplaneManager; // Ссылка на AirplaneManager
    public GameManager gameManager;         // Ссылка на GameManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
            gameManager.AddCoins(); // Начисляем монеты за попадание
        }
        else if (collision.CompareTag("border"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
            gameManager.LoseLife(); // Отнимаем жизнь за столкновение
        }
    }
}
