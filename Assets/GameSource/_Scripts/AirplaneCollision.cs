using System.Collections;
using UnityEngine;

public class AirplaneCollision : MonoBehaviour
{
    public AirplaneManager airplaneManager; // Ссылка на AirplaneManager
    public GameManager gameManager;         // Ссылка на GameManager
    public GameObject _failText;
    [SerializeField] private GameAudioCotroller _gameAudioCotroller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("target"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
            gameManager.AddCoins(); // Начисляем монеты за попадание
            _gameAudioCotroller.PlayCashSound();
        }
        else if (collision.CompareTag("border"))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
        }
        else if (collision.CompareTag("failBorder"))
        {
            StartCoroutine(ShowFail());
            gameManager.LoseLife();
            _gameAudioCotroller.PlayDeclibeSound();
        }
    }

    private IEnumerator ShowFail()
    {
        _failText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _failText.SetActive(false);
    }
}
