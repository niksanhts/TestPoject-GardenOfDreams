
using System;
using UnityEngine;

public class InputHandler : SingletonUnity<InputHandler>
{
    public Action AttackButtonEntered;
    public Action<Vector2> JoystickEntered;

    [SerializeField] private FixedJoystick joystick;

    public void EnterAttackBuuton()
        => AttackButtonEntered?.Invoke();

    private void Update()
        => JoystickEntered?.Invoke(joystick.Direction);

}
