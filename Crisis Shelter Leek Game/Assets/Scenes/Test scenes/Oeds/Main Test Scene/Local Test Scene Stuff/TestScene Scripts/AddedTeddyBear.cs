using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class AddedTeddyBear : MonoBehaviour
{
    private void Start()
    {
        UpdateTeddyBearAmount.instance.AddTeddyBearAmount();

        GetComponent<Interactable>().onInteraction.AddListener(PickUpBear);
    }

    public void PickUpBear()
    {
        UpdateTeddyBearAmount.instance.UpdateAmount();
        gameObject.SetActive(false);
    }
}
