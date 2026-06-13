using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    protected bool completed = false;

    public virtual void CompletePuzzle()
    {
        if (completed)
            return;

        completed = true;

        Debug.Log(name + " completado");

        GameManager.Instance.PuzzleCompleted();
    }
}