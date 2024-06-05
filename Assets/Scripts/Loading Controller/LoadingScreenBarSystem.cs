using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenBarSystem : MonoBehaviour
{
    public GameObject bar;
    public Text loadingText;
    public Text continueText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    public GameObject[] backgroundImages;
    [Range(0, 1f)] public float vignetteEfectVolue;
    public float loadingDuration = 5f;
    private float timer;
    private bool isLoading = false;
    private int targetSceneIndex = 2;
    private AsyncOperation async;

    private void Start()
    {
        Image vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r, vignetteEfect.color.g, vignetteEfect.color.b, vignetteEfectVolue);

        if (backGroundImageAndLoop)
            StartCoroutine(TransitionImage());

        continueText.enabled = false;
        StartLoadingNextScene();
    }

    public void StartLoadingNextScene()
    {
        timer = 0f;
        isLoading = true;
        StartCoroutine(SimulateLoading(targetSceneIndex));
    }

    private void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / loadingDuration);
            bar.transform.localScale = new Vector3(progress, 1, 1);

            if (loadingText != null)
                loadingText.text = "%" + (progress * 100).ToString("0");

            if (progress >= 1f)
            {
                isLoading = false;
                continueText.enabled = true;
                StartCoroutine(WaitForAnyKeyPress());
            }
        }
    }

    IEnumerator TransitionImage()
    {
        while (true)
        {
            for (int i = 0; i < backgroundImages.Length; i++)
            {
                yield return new WaitForSeconds(LoopTime);
                foreach (GameObject img in backgroundImages)
                    img.SetActive(false);
                backgroundImages[i].SetActive(true);
            }
        }
    }

    IEnumerator SimulateLoading(int sceneNo)
    {
        async = SceneManager.LoadSceneAsync(sceneNo);
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(loadingDuration);
        async.allowSceneActivation = true;
    }

    IEnumerator WaitForAnyKeyPress()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(targetSceneIndex);
    }
}
