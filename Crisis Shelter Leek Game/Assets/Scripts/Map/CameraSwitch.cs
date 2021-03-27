using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mapCamera;
    public GameObject player;

    [Header ("Teleportation Points")]
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;

    private void Start()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;

    }

    //switches to the map camera
    public void ShowMapView()
    {
        mainCamera.enabled = false;
        mapCamera.enabled = true;
    }
    //switches to the main camera
    public void ShowMainCameraView()
    {
        mainCamera.enabled = true;
        mapCamera.enabled = false;
    }

    //if the camera view is enabled then you can click on certain places and it will teleport you to the teleport point of the place
    void OnMouseDown()
    {
        if (mapCamera.enabled)
        {
            if(gameObject.name == "Map1")
            {
                print("map1 clicked");
                player.transform.position = new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z);
            }
            if (gameObject.name == "Map2")
            {
                print("map2 clicked");

                player.transform.position = new Vector3(point2.transform.position.x, point2.transform.position.y, point2.transform.position.z);
            }
            if (gameObject.name == "Map3")
            {
                print("map3 clicked");

                player.transform.position = new Vector3(point3.transform.position.x, point3.transform.position.y, point3.transform.position.z);
            }
        }
    }
}