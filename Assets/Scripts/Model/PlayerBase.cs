using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public float _speed;

    public abstract void Move(float x, float y, float z);
    public abstract void BabyAnim();
}
