using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private string _sceneName;

    private void Start()
    {
        StartCoroutine(SceneLoad());
    }

    private IEnumerator SceneLoad()
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(_sceneName);
        ao.allowSceneActivation = false;

        StringBuilder sb = new StringBuilder();

        while (!ao.isDone)
        {
            yield return null;

            if (ao.progress >= 0.9f)
            {
                _loadingText.text = "100%";
                ao.allowSceneActivation = true;
            }
            else
            {
                sb.Clear();
                sb.Append(ao.progress * 100f).Append("%");
                _loadingText.text = sb.ToString();
            }
        }
    }

}
