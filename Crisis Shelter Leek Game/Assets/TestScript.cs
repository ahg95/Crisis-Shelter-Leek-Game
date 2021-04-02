using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MergingSystemsNextScene");
        }
    }
}
