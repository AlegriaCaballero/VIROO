using UnityEngine;

public class CrystalPuzzle : PuzzleBase
{
    [Header("Pedestales")]
    [SerializeField] private CrystalPedestal[] pedestals;

    [Header("Audio al completar")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip completedClip;

    private void Update()
    {
        if (completed)
            return;

        if (pedestals == null || pedestals.Length == 0)
            return;

        foreach (CrystalPedestal pedestal in pedestals)
        {
            if (pedestal == null || !pedestal.IsActivated)
                return;
        }

        CompletePuzzle();
    }

    public override void CompletePuzzle()
    {
        // Evita reproducir el sonido varias veces.
        if (completed)
            return;

        if (audioSource != null && completedClip != null)
        {
            audioSource.PlayOneShot(completedClip, 1f);
        }

        // Marca el puzzle como completado y suma 1 en GameManager.
        base.CompletePuzzle();
    }
}