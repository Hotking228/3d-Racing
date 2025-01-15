using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependencies : Dependencies
{
    [SerializeField] private Pauser pauser;
    private static GlobalDependencies instance;
    private void Awake()
    {

        if(instance!= null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectsToBind();
    }

    protected override void BindAll(MonoBehaviour mono)
    {
        Bind<Pauser>(pauser, mono);
    }
}
