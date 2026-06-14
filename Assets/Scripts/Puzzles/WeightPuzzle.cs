using UnityEngine;
using System.Collections;

public class WeightPuzzle : PuzzleBase
{
    [Header("Mano que sostiene el cristal")]
    public Transform movingHand;
    private Coroutine currentMove;

    [Header("Alturas")]
    public float swordY = 2.4f;
    public float shieldY = 2.8f;
    public float helmetY = 0.6f;

    [Header("Cristal recompensa")]
    public Rigidbody rewardCrystal;

    public void PlaceSword()
    {
        if (completed)
            return;

        MoveHand(swordY);
    }

    public void PlaceShield()
    {
        if (completed)
            return;

        MoveHand(shieldY);
    }

    public void PlaceHelmet()
    {
        if (completed)
            return;

        MoveHand(helmetY);

        if (rewardCrystal != null)
        {
            rewardCrystal.isKinematic = false;
        }

        CompletePuzzle();
    }

    private void MoveHand(float targetY)
    {
        if (currentMove != null)
        {
            StopCoroutine(currentMove);
        }

        currentMove = StartCoroutine(MoveHandRoutine(targetY));
    }

    private IEnumerator MoveHandRoutine(float targetY)
    {
        float speed = 1.5f;

        Vector3 targetPosition = movingHand.position;
        targetPosition.y = targetY;

        while (Vector3.Distance(movingHand.position, targetPosition) > 0.01f)
        {
            movingHand.position = Vector3.MoveTowards(
                movingHand.position,
                targetPosition,
                speed * Time.deltaTime);

            yield return null;
        }

        movingHand.position = targetPosition;
    }
}