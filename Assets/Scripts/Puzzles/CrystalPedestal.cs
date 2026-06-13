using UnityEngine;

public class CrystalPedestal : MonoBehaviour
{
    public CrystalType requiredType;

    public bool IsActivated { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActivated)
            return;

        Crystal crystal = other.GetComponent<Crystal>();

        if (crystal == null)
            return;

        if (crystal.crystalType == requiredType)
        {
            IsActivated = true;

            Debug.Log(requiredType + " colocado correctamente");
        }
    }
}