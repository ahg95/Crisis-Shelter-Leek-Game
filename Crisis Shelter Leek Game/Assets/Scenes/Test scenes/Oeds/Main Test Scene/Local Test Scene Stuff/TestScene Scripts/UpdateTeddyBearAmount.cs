using TMPro;
using UnityEngine;

public class UpdateTeddyBearAmount : MonoBehaviour
{
    public int amountOfTeddyBears;
    public int teddyBearsCollected = 0;
    public static UpdateTeddyBearAmount instance;
    [SerializeField] private TextMeshProUGUI amountCounter;
    private int quoteToPlay = 0;
    [Space(10)]
    [SerializeField] private AudioSource quotePlayer;
    [SerializeField] private AudioClip[] quotes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (quotePlayer == null)
        {
            quotePlayer = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        }
    }
    private void Start()
    {
        amountCounter.text = teddyBearsCollected + " / " + amountOfTeddyBears;
    }
    public void UpdateAmount()
    {
        teddyBearsCollected++;

        quotePlayer.PlayOneShot(quotes[quoteToPlay]);

        if (quoteToPlay < quotes.Length - 1)
        {
            quoteToPlay++;
        }
        else
        {
            quoteToPlay = 0;
        }

        // print("You've collected " + teddyBearsCollected + " out of the " + amountOfTeddyBears + " teddybears!");
        amountCounter.text = teddyBearsCollected + " / " + amountOfTeddyBears;
    }


}
