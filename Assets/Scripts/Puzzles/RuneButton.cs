using UnityEngine;

public class RuneButton : MonoBehaviour
{
    public int runeIndex;

    public Light runeLight;

    private RunePuzzle puzzle;

    private void Start()
    {
        puzzle = FindFirstObjectByType<RunePuzzle>();

        if (runeLight != null)
            runeLight.intensity = 0;
    }

    public void PressRune()
    {
        puzzle.PressRune(this);
    }

    public void TurnOn()
    {
        if (runeLight != null)
            runeLight.intensity = 1f;
    }

    public void TurnOff()
    {
        if (runeLight != null)
            runeLight.intensity = 0f;
    }
}