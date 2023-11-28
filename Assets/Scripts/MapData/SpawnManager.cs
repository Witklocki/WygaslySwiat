using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance; // Singleton pattern for easy access

    [System.Serializable]
    public class SceneSpawnData
    {
        public string sceneName;
        public Vector3 defaultSpawnPosition;
        public Vector3 customSpawnPosition;
    }

    public SceneSpawnData[] sceneSpawnData;
    public Vector3 customSpawnPosition; // This line was missing
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the spawn position for the current scene
        SetSpawnPosition(scene.name);
    }

    private void SetSpawnPosition(string sceneName)
    {
        // Find the SceneSpawnData for the current scene
        SceneSpawnData currentSceneSpawnData = System.Array.Find(sceneSpawnData, data => data.sceneName == sceneName);

        if (currentSceneSpawnData != null)
        {
            // Set the spawn position based on whether there's a custom spawn position
            customSpawnPosition = currentSceneSpawnData.customSpawnPosition != Vector3.zero
                ? currentSceneSpawnData.customSpawnPosition
                : currentSceneSpawnData.defaultSpawnPosition;

            Debug.Log("Spawn Position set to: " + customSpawnPosition);
        }
        else
        {
            Debug.LogWarning("Spawn data not found for scene: " + sceneName);
        }
    }
}
