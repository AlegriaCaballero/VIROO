using UnityEngine;

public class RunePuzzle : PuzzleBase
{
    [Header("Runas")]
    public RuneButton[] runes;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctClip;
    public AudioClip errorClip;
    public AudioClip completedClip;

    private int currentStep;

    private readonly int[] correctOrder =
    {
        0,
        1,
        2,
        3
    };

    public void PressRune(RuneButton rune)
    {
        if (completed || rune == null)
            return;

        if (currentStep >= correctOrder.Length)
            return;

        if (rune.runeIndex == correctOrder[currentStep])
        {
            rune.TurnOn();
            currentStep++;

            if (currentStep >= correctOrder.Length)
            {
                PlaySound(completedClip, 1f);
                CompletePuzzle();
            }
            else
            {
                PlaySound(correctClip, 0.7f);
            }

            return;
        }

        ResetPuzzle();
    }

    private void ResetPuzzle()
    {
        currentStep = 0;

        foreach (RuneButton rune in runes)
        {
            if (rune != null)
                rune.TurnOff();
        }

        PlaySound(errorClip, 0.8f);

        Debug.Log("Secuencia incorrecta");
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip, volume);
    }
}