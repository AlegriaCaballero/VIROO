using System.Collections;
using UnityEngine;

public class FinalSequenceController : MonoBehaviour
{
    [Header("Cristal final")]
    [SerializeField] private GameObject finalCrystal;
    [SerializeField] private Transform crystalTransform;

    [Header("Movimiento del cristal")]
    [SerializeField] private Transform crystalFinalPoint;
    [SerializeField] private float crystalRiseSpeed = 1f;

    [Header("Efectos")]
    [SerializeField] private ParticleSystem unlockParticles;
    [SerializeField] private ParticleSystem victoryParticles;
    [SerializeField] private Light altarLight;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip altarUnlockedClip;
    [SerializeField] private AudioClip victoryClip;

    [Header("Resultado final")]
    [SerializeField] private GameObject victoryMessage;
    [SerializeField] private Transform exitDoor;
    [SerializeField] private float doorOpenHeight = 4f;
    [SerializeField] private float doorSpeed = 1.5f;

    private bool altarUnlocked;
    private bool gameFinished;

    private void Awake()
    {
        if (finalCrystal != null)
            finalCrystal.SetActive(false);

        if (victoryMessage != null)
            victoryMessage.SetActive(false);

        if (altarLight != null)
            altarLight.intensity = 0f;
    }

    public void UnlockAltar()
    {
        if (altarUnlocked)
            return;

        altarUnlocked = true;

        if (finalCrystal != null)
            finalCrystal.SetActive(true);

        if (unlockParticles != null)
            unlockParticles.Play();

        if (altarLight != null)
            altarLight.intensity = 6f;

        if (audioSource != null && altarUnlockedClip != null)
            audioSource.PlayOneShot(altarUnlockedClip);

        if (crystalTransform != null && crystalFinalPoint != null)
            StartCoroutine(RaiseCrystal());

        Debug.Log("El Altar del Monarca ha sido desbloqueado.");
    }

    public void ClaimFinalCrystal()
    {
        if (!altarUnlocked || gameFinished)
            return;

        gameFinished = true;

        if (victoryParticles != null)
            victoryParticles.Play();

        if (audioSource != null && victoryClip != null)
            audioSource.PlayOneShot(victoryClip);

        if (victoryMessage != null)
            victoryMessage.SetActive(true);

        if (exitDoor != null)
            StartCoroutine(OpenExitDoor());

        Debug.Log("Cristal del Monarca obtenido. Experiencia completada.");
    }

    private IEnumerator RaiseCrystal()
    {
        while (Vector3.Distance(
                   crystalTransform.position,
                   crystalFinalPoint.position) > 0.01f)
        {
            crystalTransform.position = Vector3.MoveTowards(
                crystalTransform.position,
                crystalFinalPoint.position,
                crystalRiseSpeed * Time.deltaTime
            );

            crystalTransform.Rotate(
                Vector3.up,
                40f * Time.deltaTime,
                Space.World
            );

            yield return null;
        }

        crystalTransform.position = crystalFinalPoint.position;
    }

    private IEnumerator OpenExitDoor()
    {
        Vector3 targetPosition = exitDoor.position;
        targetPosition.y += doorOpenHeight;

        while (Vector3.Distance(
                   exitDoor.position,
                   targetPosition) > 0.01f)
        {
            exitDoor.position = Vector3.MoveTowards(
                exitDoor.position,
                targetPosition,
                doorSpeed * Time.deltaTime
            );

            yield return null;
        }

        exitDoor.position = targetPosition;
    }
}