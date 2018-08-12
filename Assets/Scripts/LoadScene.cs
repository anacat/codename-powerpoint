using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadScene : MonoBehaviour
{
    public Animator fadeAnimator;

    private bool isInTransition = false;

    public int sceneToLoad = 0;
    public int badSceneToLoad = 0;
    public int badSceneToLoad2 = 0;
    public int badSceneToLoad3 = 0;
    public int previousLevel;

    private void Awake()
    {
        if(sceneToLoad == -1)
        {
            sceneToLoad = Int32.Parse(SceneManager.GetActiveScene().name) + 1;
        }

        if(previousLevel == 0)
        { 
            previousLevel = sceneToLoad - 2;
        }
    }

    public void LoadSceneAsync(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void StartBadTransition(int levelOfBad)
    {
        switch(levelOfBad)
        {
            case 1: 
                StartTransition(badSceneToLoad);
                break;
            case 2: 
                StartTransition(badSceneToLoad2);
                break;
            case 3:
                StartTransition(badSceneToLoad3);
                break;
            default:
                break;
        }
    }

    public void StartPreviousTransition()
    {
        StartTransition(previousLevel);
    }

    public void StartTransition()
    {
        StartTransition(sceneToLoad);
    }

    public void StartTransition(int scene)
    {
        if (isInTransition)
            return;

        StopAllCoroutines();
        StartCoroutine(Transition(scene));
    }

    public IEnumerator Transition(int scene)
    {
        AsyncOperation asyncOperation;

        DontDestroyOnLoad(gameObject);

        fadeAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(2);

        asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        isInTransition = false;
    }
}
