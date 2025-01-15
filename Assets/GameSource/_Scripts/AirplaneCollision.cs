using UnityEngine;

public class AirplaneCollision : MonoBehaviour
{
    public AirplaneManager airplaneManager; // Ссылка на AirplaneManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (airplaneManager != null && (collision.CompareTag("target") || collision.CompareTag("border")))
        {
            airplaneManager.OnAirplaneCollision(collision.tag);
        }
    }
}