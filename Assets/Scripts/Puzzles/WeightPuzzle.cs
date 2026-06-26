using System.Collections;
using UnityEngine;

public class WeightPuzzle : PuzzleBase
{
    [Header("Mano que sostiene el cristal")]
    public Transform movingHand;

    [Header("Alturas")]
    public float swordY = 2.4f;
    public float shieldY = 2.8f;
    public float helmetY = 0.6f;

    [Header("Movimiento")]
    public float movementSpeed = 1.5f;

    [Header("Cristal recompensa")]
    public Rigidbody rewardCrystal;

    [Header("Audio")]
    public AudioSource mechanismAudioSource;
    public AudioClip movingLoopClip;
    public AudioClip completedClip;

    private Coroutine currentMovement;

    public void PlaceSword()
    {
        MoveHand(swordY, false);
    }

    public void PlaceShield()
    {
        MoveHand(shieldY, false);
    }

    public void PlaceHelmet()
    {
        MoveHand(helmetY, true);
    }

    private void MoveHand(float targetY, bool completeAtEnd)
    {
        if (completed)
            return;

        if (currentMovement != null)
            StopCoroutine(currentMovement);

        currentMovement = StartCoroutine(
            MoveHandRoutine(targetY, completeAtEnd)
        );
    }

    private IEnumerator MoveHandRoutine(
        float targetY,
        bool completeAtEnd)
    {
        if (movingHand == null)
            yield break;

        StartMovementAudio();

        Vector3 targetPosition = movingHand.position;
        targetPosition.y = targetY;

        while (Vector3.Distance(
                   movingHand.position,
                   targetPosition) > 0.01f)
        {
            movingHand.position = Vector3.MoveTowards(
                movingHand.position,
                targetPosition,
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }

        movingHand.position = targetPosition;

        StopMovementAudio();

        if (completeAtEnd && !completed)
        {
            if (rewardCrystal != null)
                rewardCrystal.isKinematic = false;

            if (mechanismAudioSource != null &&
                completedClip != null)
            {
                mechanismAudioSource.PlayOneShot(
                    completedClip,
                    0.9f
                );
            }

            CompletePuzzle();
        }

        currentMovement = null;
    }

    private void StartMovementAudio()
    {
        if (mechanismAudioSource == null ||
            movingLoopClip == null)
            return;

        mechanismAudioSource.clip = movingLoopClip;
        mechanismAudioSource.loop = true;

        if (!mechanismAudioSource.isPlaying)
            mechanismAudioSource.Play();
    }

    private void StopMovementAudio()
    {
        if (mechanismAudioSource == null)
            return;

        mechanismAudioSource.Stop();
        mechanismAudioSource.loop = false;
        mechanismAudioSource.clip = null;
    }
}