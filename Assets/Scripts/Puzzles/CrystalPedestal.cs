using UnityEngine;

public class CrystalPedestal : MonoBehaviour
{
    public CrystalType requiredType;

    public bool IsActivated { get; private set; }

    [Header("Snap Point")]
    public Transform snapPoint;

    [Header("Efectos")]
    public Light pedestalLight;

    private Crystal currentCrystal;

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

            currentCrystal = crystal;

            crystal.transform.position = snapPoint.position;
            crystal.transform.rotation = snapPoint.rotation;

            if (pedestalLight != null)
            {
                pedestalLight.intensity = 0.5f;
            }

            Debug.Log(requiredType + " colocado correctamente");
        }
    }
}