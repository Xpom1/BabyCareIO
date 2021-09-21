using UnityEngine;

public class PlayerBaby : PlayerBase
{
    private Rigidbody _rigidbodyBaby;
    private Animator _babyAnim;
  
    [SerializeField]
    private Joystick _joystick;

    private void Awake()
    {
        _rigidbodyBaby = GetComponent<Rigidbody>();
        _babyAnim = GetComponent<Animator>();
    }
    public override void Move(float x, float y, float z)
    {
        _rigidbodyBaby.velocity = new Vector3(x, y, z) * _speed;  
    }
    public override void BabyAnim()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _babyAnim.SetBool("Walk", true);
        }
        if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
            _babyAnim.SetBool("Walk", false);
    }

    public override void RotationMove()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbodyBaby.velocity);
        }
    }
}
