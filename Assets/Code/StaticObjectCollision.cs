using UnityEngine;

public class StaticObjectCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

            Debug.Log("Статичный объект столкнулся с падающим шаром!");
            // Здесь можно добавить вашу логику:
            // например, вызвать эффект, запустить анимацию, изменить состояние и т.д.
        
    }

    // Если используете триггер-коллайдер
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FallingBall"))
        {
            Debug.Log("Статичный триггер зафиксировал пересечение с падающим шаром!");
            // Дополнительные действия при пересечении триггера
        }
    }
}