using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _movementSpeed = 5f;
    // offset the arc from the middle of the platform
    [SerializeField] private float _platformHeightOffset = 0.5f;
    [SerializeField] private float _maxDistanceFromCameraBeforeDeath = 5f;
    private Rigidbody2D _rigidbody;
    private InputAction _moveInputAction;
    private float _horizontalLength = 2.88675f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInputAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if ((transform.position.y + _maxDistanceFromCameraBeforeDeath) <= Camera.main.transform.position.y)
        {
            gameObject.SetActive(false);
        } 
        if ((transform.position.x < -_horizontalLength))
        {
            transform.position = new Vector2(_horizontalLength, transform.position.y);
        }
        if ((transform.position.x > _horizontalLength))
        {
            transform.position = new Vector2(-_horizontalLength, transform.position.y);
        }
    }
    private void FixedUpdate()
    {
        Vector2 moveInput = _moveInputAction.ReadValue<Vector2>();
        _rigidbody.linearVelocity = new Vector2(moveInput.x * _movementSpeed, _rigidbody.linearVelocity.y);
    }
    // Update is called once per frame
    // assure the player need to be above the platform + platform height offset
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (transform.position.y > (other.transform.position.y + _platformHeightOffset))
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
             _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
       
    }

}
