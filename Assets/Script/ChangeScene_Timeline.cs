using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Timeline : MonoBehaviour
{
    public string scenename;
    void OnEnable()
    {
        SceneManager.LoadScene(scenename);
    }
}
