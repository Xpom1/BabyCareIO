using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour, IExecute
{
    private bool _isInteractable = true;
    protected abstract void Interaction();


    private void OnTriggerEnter(Collider other)
    {
        if (!_isInteractable || !other.CompareTag("Player"))
        {
            return;
        }
        Interaction();
        _isInteractable = false;
    }
    public abstract void Execute();

}
