using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class AddedTeddyBear : MonoBehaviour
{

    private void OnEnable()
    {
        UpdateTeddyBearAmount.instance.amountOfTeddyBears++;

        GetComponent<Interactable>().onInteraction.AddListener(PickUpBear);
    }

    public void PickUpBear()
    {
        UpdateTeddyBearAmount.instance.UpdateAmount();
        gameObject.SetActive(false);
    }
}
