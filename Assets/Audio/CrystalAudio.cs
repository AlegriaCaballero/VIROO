using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CrystalAudio : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip grabClip;
    [SerializeField] private AudioClip releaseClip;
    [SerializeField] private AudioClip impactClip;

    [Header("Configuración")]
    [SerializeField, Range(0f, 1f)]
    private float volume = 0.75f;

    [SerializeField]
    private Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    [SerializeField, Min(0f)]
    private float minimumImpactVelocity = 1.2f;

    [SerializeField, Min(0f)]
    private float impactCooldown = 0.15f;

    private AudioSource audioSource;
    private float nextImpactTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayGrab()
    {
        PlayClip(grabClip);
    }

    public void PlayRelease()
    {
        PlayClip(releaseClip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time < nextImpactTime)
            return;

        if (collision.relativeVelocity.magnitude < minimumImpactVelocity)
            return;

        nextImpactTime = Time.time + impactCooldown;

        float impactVolume = Mathf.Clamp01(
            collision.relativeVelocity.magnitude / 6f
        );

        PlayClip(impactClip, impactVolume);
    }

    private void PlayClip(AudioClip clip, float volumeMultiplier = 1f)
    {
        if (clip == null || audioSource == null)
            return;

        audioSource.pitch = Random.Range(
            pitchRange.x,
            pitchRange.y
        );

        audioSource.PlayOneShot(
            clip,
            volume * volumeMultiplier
        );
    }
}