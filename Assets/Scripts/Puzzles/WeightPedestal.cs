using UnityEngine;

public class WeightPedestal : MonoBehaviour
{
    public WeightPuzzle puzzle;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        WeightItem item = other.GetComponent<WeightItem>();

        if (item == null)
            return;

        switch (item.itemType)
        {
            case ItemType.Sword:

                Debug.Log("Espada colocada");
                puzzle.PlaceSword();
                break;

            case ItemType.Shield:

                Debug.Log("Escudo colocado");
                puzzle.PlaceShield();
                break;

            case ItemType.Helmet:

                Debug.Log("Casco correcto");

                activated = true;

                puzzle.PlaceHelmet();

                break;
        }
    }
}