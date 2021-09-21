public class PlayerController : IExecute
{
    private readonly PlayerBase _playerBase;
    private readonly Joystick _joystick;

    public PlayerController(PlayerBase player, Joystick joystick)
    {
        _playerBase = player;
        _joystick = joystick;
        
    }
    public void Execute()
    {
        _playerBase.Move(_joystick.Horizontal, 0, _joystick.Vertical);
        _playerBase.BabyAnim();
        _playerBase.RotationMove();
    }
   

    
}
