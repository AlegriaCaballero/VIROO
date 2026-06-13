using UnityEngine;

public class CrystalPuzzle : PuzzleBase
{
    public CrystalPedestal[] pedestals;

    public GameObject rewardObject;

    private bool rewardSpawned = false;

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

        if (!rewardSpawned)
        {
            rewardSpawned = true;

            if (rewardObject != null)
            {
                rewardObject.SetActive(true);
            }
        }
    }
}