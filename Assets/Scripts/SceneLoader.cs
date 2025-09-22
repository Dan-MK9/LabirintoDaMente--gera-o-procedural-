using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader singletonIstance;

    private bool isLoadingInProgress;

    public static SceneLoader Instance
    {
        get
        {
            if (singletonIstance == null)
            {
                var go = new GameObject("[SceneLoader]");

                singletonIstance = go.AddComponent<SceneLoader>();

                DontDestroyOnLoad(go);
            }

            return singletonIstance;
        }
    }

    public static void Load(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;

        if (Instance.isLoadingInProgress) return;

        Instance.StartCoroutine(Instance.LoadRoutine(sceneName));
    }

    private IEnumerator LoadRoutine(string targetSceneName)
    {
        isLoadingInProgress = true;

        var loadOperation = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);

        loadOperation.allowSceneActivation = true;

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        yield return null;

        isLoadingInProgress = false;
    }
}
