using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dependencies : MonoBehaviour
{
    protected virtual void BindAll(MonoBehaviour mono) { }
    protected virtual void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target)where T : class
    {
        if (target is IDependency<T>) (target as IDependency<T>).Construct(bindObject as T);
    }

    protected void FindAllObjectsToBind()
    {
        MonoBehaviour[] monoInScene = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        for (int i = 0; i < monoInScene.Length; i++)
        {
            BindAll(monoInScene[i]);
        }
    }




}
