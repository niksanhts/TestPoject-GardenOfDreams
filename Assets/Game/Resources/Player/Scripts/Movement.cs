using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _rigidbody;

    public void Initialize(float speed)
    {
        _speed = speed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
        => InputHandler.Instance.JoystickEntered += Move;

    private void OnDisable()
        => InputHandler.Instance.JoystickEntered -= Move;

    private void Move(Vector2 direction) 
    {
        _rigidbody.MovePosition(new Vector3(direction.x, direction.y, 0) * _speed * Time.deltaTime + transform.position);
        
    }
    
}
