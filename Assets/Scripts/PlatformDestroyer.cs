using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
   [SerializeField] private float _maxDistanceFromCamera = 5f;

   private void Update()
    {
        if (Camera.main.transform.position.y > transform.position.y + _maxDistanceFromCamera)
        {
            Destroy(gameObject);
        }
    }
    
}
