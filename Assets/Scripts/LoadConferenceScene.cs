using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConferenceScene : MonoBehaviour {
    
	void Start () {
        SceneManager.LoadSceneAsync("ConferenceCinema");
	}
}
