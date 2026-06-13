using UnityEngine;

public class CrystalPuzzle : PuzzleBase
{
    public CrystalPedestal[] pedestals;

    private void Update()
    {
        if (completed)
            return;

        foreach (var pedestal in pedestals)
        {
            if (!pedestal.IsActivated)
                return;
        }

        CompletePuzzle();
    }
}