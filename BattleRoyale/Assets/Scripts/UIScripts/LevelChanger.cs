using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;

    private int levelToLoad;
    string sceneToLoad;

	// Update is called once per frame
	void Update () {
		
	}

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }
    public void FadeToLevel(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

     public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
