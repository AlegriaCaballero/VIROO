using UnityEngine;

public class RunePuzzle : PuzzleBase
{
    public RuneButton[] runes;

    private int currentStep = 0;

    private int[] correctOrder =
    {
        0,
        1,
        2,
        3
    };

    public void PressRune(RuneButton rune)
    {
        if (completed)
            return;

        if (rune.runeIndex == correctOrder[currentStep])
        {
            rune.TurnOn();

            currentStep++;

            Debug.Log("Runa correcta");

            if (currentStep >= correctOrder.Length)
            {
                CompletePuzzle();
            }
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void ResetPuzzle()
    {
        currentStep = 0;

        foreach (RuneButton rune in runes)
        {
            rune.TurnOff();
        }

        Debug.Log("Secuencia incorrecta");
    }
}