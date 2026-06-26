using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Progreso")]
    [SerializeField] private int completedPuzzles;

    [Header("Configuración")]
    [SerializeField] private int totalPuzzles = 3;

    [Header("Secuencia final")]
    [SerializeField] private FinalSequenceController finalSequence;

    private bool finalActivated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PuzzleCompleted()
    {
        if (finalActivated)
            return;

        completedPuzzles++;

        Debug.Log(
            $"Puzzle completado: {completedPuzzles}/{totalPuzzles}"
        );

        if (completedPuzzles >= totalPuzzles)
        {
            finalActivated = true;

            if (finalSequence != null)
                finalSequence.UnlockAltar();
            else
                Debug.LogWarning(
                    "No se asignó FinalSequence en GameManager."
                );
        }
    }
}