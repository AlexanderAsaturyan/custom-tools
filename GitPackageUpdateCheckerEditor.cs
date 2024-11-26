using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class GitPackageUpdateCheckerEditor : MonoBehaviour
    {
        static GitPackageUpdateCheckerEditor()
        {
            Debug.LogError("GitPackageUpdateCheckerEditor Constructor");
            // Perform the check for updates when the editor starts
            CheckForUpdate();
        }

        // Asynchronous method to check for updates
        private static async void CheckForUpdate()
        {
            Debug.LogError("CheckForUpdate");
            string latestVersion = await GetLatestVersionAsync();
        
            // Current version of your package, replace with the actual version you're using
            string currentVersion = "1.0.0"; 

            if (latestVersion != null && latestVersion != currentVersion)
            {
                Debug.LogError("CheckForUpdate IF");
                Debug.LogWarning($"[MyCustomTool] Update available! Installed: {currentVersion}, Latest: {latestVersion}. " +
                                 "To update, modify the manifest.json file and reference the latest tag.");
            }
        }

        // Fetches the latest release version from GitHub
        private static async Task<string> GetLatestVersionAsync()
        {
            Debug.LogError("GetLatestVersionAsync");
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Unity");
            try
            {
                Debug.LogError("GetLatestVersionAsync TRY");
                // Replace this URL with the URL of your package’s GitHub repository
                string response = await client.GetStringAsync("https://github.com/AlexanderAsaturyan/custom-tools.git");
                Debug.LogError("Client: " + client);
                Debug.LogError("Response: " + response);
                GitHubRelease release = JsonUtility.FromJson<GitHubRelease>(response);
                Debug.LogError("release: " + release);
                Debug.LogError("Release TagName: " + release.tag_name);
                return release.tag_name;
            }
            catch
            {
                Debug.LogError("GetLatestVersionAsync CATCH");
                Debug.LogWarning("[MyCustomTool] Unable to check for updates. Ensure you have an internet connection.");
                return null;
            }
        }

        // GitHub release model to parse the version tag
        [System.Serializable]
        private class GitHubRelease
        {
            public string tag_name;
        }
    }
}
