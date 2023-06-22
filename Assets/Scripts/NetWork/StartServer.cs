using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartServer : MonoBehaviour {
    void Start() {
#if DEDICATED_SERVER
        Debug.Log("start server");
        SceneManager.LoadScene(SceneName.GameScene, LoadSceneMode.Single);
#endif
    }
}
