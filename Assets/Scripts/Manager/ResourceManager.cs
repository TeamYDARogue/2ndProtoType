using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {

    class ResourceData
    {
        public string resourceName;
        public GameObject resource;

        public ResourceData(string name,GameObject resource)
        {
            resourceName = name;
            this.resource = resource;
        }
    }

    private static ResourceManager m_instance = null;
    public static ResourceManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                var obj = new GameObject("ResourceManager");
                m_instance = obj.AddComponent<ResourceManager>();
                m_instance.GetComponent<ResourceManager>().enabled = true;
            }
            return m_instance;
        }
    }



    private GameObject[] m_resouce;
    private Dictionary<string, ResourceData> m_sceneResources;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_sceneResources = new Dictionary<string, ResourceData>();

    }


    public void ResourcesLoad(string sceneName)
    {

        m_sceneResources.Clear();


        m_resouce = Resources.LoadAll<GameObject>("Prefabs/" + sceneName);
        foreach(var obj in m_resouce)
        {
            m_sceneResources.Add(obj.name, new ResourceData("Prefabs/" + obj.name, obj));
            Debug.Log(obj.name + "Loaded!!!");
        }

        m_resouce = null;
    }

    public GameObject GetResourceScene(string key)
    {
        if(m_sceneResources.ContainsKey(key))
        {
            return m_sceneResources[key].resource;
        }

        return null;
    }

    public void ResourcesUnLoad()
    {
        StartCoroutine(UnLoadResources());
    }

    IEnumerator UnLoadResources()
    {
        foreach(KeyValuePair<string,ResourceData> pair in m_sceneResources)
        {
            Resources.UnloadAsset(pair.Value.resource);
            yield return null;
        }
    }
}
