using System.Collections;
using UnityEngine;

public class AirplaneCollision : MonoBehaviour
{
    public AirplaneManager airplaneManager; // Ссылка на AirplaneManager
    public GameManager gameManager;         // Ссылка на GameManager
    public GameObject _failText;

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
        }
        else if (collision.CompareTag("failBorder"))
        {
            StartCoroutine(ShowFail());
            gameManager.LoseLife();
        }
    }

    private IEnumerator ShowFail()
    {
        _failText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _failText.SetActive(false);
    }
}
