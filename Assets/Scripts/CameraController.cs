using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    

    private void Start()
    {
        Camera.main.orthographicSize = (2.88675f) / Camera.main.aspect;
    }
    private void LateUpdate()
    {
        if (transform.position.y < _player.position.y)
        {
            transform.position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }
}
