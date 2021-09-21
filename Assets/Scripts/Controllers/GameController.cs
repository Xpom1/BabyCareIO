using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private PlayerBaby _baby;
    private ListExecuteObject _interactiveObject;
    private PlayerController _inputController;
    
    private void Awake()
    {
        _interactiveObject = new ListExecuteObject();
        _inputController = new PlayerController(_baby,_joystick);
        _interactiveObject.AddExecuteObject(_inputController);
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < _interactiveObject.Length; i++)
        {
            var interactiveObject = _interactiveObject[i];

            if (interactiveObject == null)
            {
                continue;
            }
            interactiveObject.Execute();
        }

    }
}
