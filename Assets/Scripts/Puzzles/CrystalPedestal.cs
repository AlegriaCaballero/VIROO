using UnityEngine;

public class CrystalPedestal : MonoBehaviour
{
    public CrystalType requiredType;

    public bool IsActivated { get; private set; }

    [Header("Snap Point")]
    public Transform snapPoint;

    [Header("Efectos")]
    public Light pedestalLight;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip crystalPlacedClip;

    private Crystal currentCrystal;

    private void OnTriggerEnter(Collider other)
    {
        if (IsActivated)
            return;

        Crystal crystal = other.GetComponent<Crystal>();

        if (crystal == null)
            crystal = other.GetComponentInParent<Crystal>();

        if (crystal == null)
            return;

        if (crystal.crystalType != requiredType)
            return;

        IsActivated = true;
        currentCrystal = crystal;

        crystal.transform.position = snapPoint.position;
        crystal.transform.rotation = snapPoint.rotation;

        if (pedestalLight != null)
            pedestalLight.intensity = 5f;

        if (audioSource != null && crystalPlacedClip != null)
            audioSource.PlayOneShot(crystalPlacedClip, 0.8f);

        Debug.Log(requiredType + " colocado correctamente");
    }
}