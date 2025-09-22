using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent (typeof(Collider2D))]

public class PortalManager : MonoBehaviour
{
    public string targetSceneName;

    public string playerTagName = "Player";

    private bool isTransitionInProgrss = false;

    private Collider2D[] portalColliders;

    void Awake()
    {
        portalColliders = GetComponentsInChildren<Collider2D>();

        foreach (var collider in portalColliders) {
            if (collider) collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTagName)) return;

        if (isTransitionInProgrss) return;

        isTransitionInProgrss = true;

        foreach (var collider in portalColliders)
        {
            if (collider) collider.enabled = false;
        }

        if (!string.IsNullOrEmpty(targetSceneName)) 
        {
            SceneLoader.Load(targetSceneName);
        }
        else
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = (currentScene + 1 < SceneManager.sceneCountInBuildSettings)
            ? currentScene + 1 : 0;

            string nextPath = SceneUtility.GetScenePathByBuildIndex (nextScene);
            string nextName = Path.GetFileNameWithoutExtension (nextPath);

            SceneLoader.Load(nextName);
        }
    }

}
