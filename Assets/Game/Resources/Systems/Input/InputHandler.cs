
using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action AttackButtonEntered;
    public Action<Vector2> JoystickEntered;

    [SerializeField] private FixedJoystick joystick;

    public void EnterAttackButton()
        => AttackButtonEntered?.Invoke();

    private void Update()
        => JoystickEntered?.Invoke(joystick.Direction);

}
