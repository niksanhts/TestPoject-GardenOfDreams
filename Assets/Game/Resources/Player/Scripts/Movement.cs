using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _rigidbody;
    private InputHandler _inputHandler;

    public void Initialize(float speed, InputHandler inputHandler)
    {
        _speed = speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputHandler = inputHandler;
        _inputHandler.JoystickEntered += Move;

        try
        {
            transform.position = Load();
        }
        catch
        {
            transform.position = new Vector3();
        }
    }


    private void OnDisable()
    {
        Save();
        _inputHandler.JoystickEntered -= Move;
    }



    private void Move(Vector2 direction) 
    {
        _rigidbody.MovePosition(new Vector3(direction.x, direction.y, 0) * _speed * Time.deltaTime + transform.position);
    }

    private void Save()
    {
        MyVector3 position = new MyVector3(
        
            transform.position.x,
            transform.position.y,
            transform.position.z
        );
        Storage.Save(gameObject.name, "position", position);
    }

    private Vector3 Load()
    {
        MyVector3 loadPosition = (MyVector3)Storage.Load(gameObject.name, "position");
        return new Vector3(loadPosition.X, loadPosition.Y, loadPosition.Z);
    }
    
}
