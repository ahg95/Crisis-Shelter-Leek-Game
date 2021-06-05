using UnityEngine;
using System.ComponentModel;

public class SpawnPoint : MonoBehaviour
{
    public enum _SpawnPoint
    {
        [Description("Entrance Wender")]
        entranceWender,
        [Description("Bedroom Door Front Desk")]
        bedRoomDoorFrontDesk,
        [Description("Bed Bedroom")]
        bedBedroom,
        [Description("Door Bedroom")]
        doorBedroom,
        [Description("Entrance Municipality")]
        entranceMunicipality
    }

    public _SpawnPoint thisSpawnPoint;

    private void OnValidate()
    {
        System.Enum spawnPointEnum = thisSpawnPoint;
        var toString = spawnPointEnum.ToString();
        var type = spawnPointEnum.GetType();
        var field = type.GetField(toString);
        var attrib = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        gameObject.name = attrib.Description;
    }
}
