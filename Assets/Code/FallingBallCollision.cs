using UnityEngine;

public class FallingBallCollision : MonoBehaviour
{
    public AudioClip sound;
    // Этот метод вызывается при соприкосновении физического коллайдера
    private void OnCollisionEnter(Collision collision)
    {
print(collision.gameObject.name);        if (collision.gameObject.CompareTag("BlueBall"))
        {
            AudioManager.Instance.PlaySound(sound);
        }
    }
    
}