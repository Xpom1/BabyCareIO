using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : IExecute
{
    private readonly PlayerBase _playerBase;
    private readonly Joystick _joystick;

    public InputController(PlayerBase player, Joystick joystick)
    {
        _playerBase = player;
        _joystick = joystick;
        
    }
    public void Execute()
    {
        _playerBase.Move(_joystick.Horizontal, 0, _joystick.Vertical);
        _playerBase.BabyAnim();
    }
   

    
}
