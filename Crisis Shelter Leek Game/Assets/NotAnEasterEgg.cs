using UnityEngine;

public class NotAnEasterEgg : MonoBehaviour
{
    [SerializeField] private AudioClip firstClip;
    [SerializeField] private AudioClip secondClip;

    [SerializeField] private Transform door;

    private int amountOfClicks = 0;
    private GameObject objectBehindPlayer;
    private AudioSource source;

    private void Start()
    {
        objectBehindPlayer = new GameObject();
        objectBehindPlayer.transform.position = door.position;
        source = objectBehindPlayer.AddComponent<AudioSource>();
        source.spatialBlend = 1f;
    }

    public void NotAnEasterEggVoid()
    {
        amountOfClicks++;

        if (amountOfClicks == 5)
        {
            source.clip = firstClip;
            source.Play();
        }

        if (amountOfClicks == 10)
        {
            source.clip = secondClip;
            source.Play();

            amountOfClicks = 0;
        }
    }
}
