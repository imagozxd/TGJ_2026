using System.Collections.Generic;
using UnityEngine;

public interface IPoolSystem {
    public GameObject PoolObject(Vector3 position, Quaternion rotation);
    public void ReturnToQueue(GameObject obj);
}

/// <summary>
/// General class for managing different PoolSystems 
/// (I should make a "PoolSystemManager" for accessing the different PoolSystems created here)
/// </summary>
/// <typeparam name="T">Class of the specific PoolSystem</typeparam>
public abstract class PoolSystem<T> : StaticInstance<T>, 
    IPoolSystem where T : MonoBehaviour {
    [SerializeField] protected int m_numberOfInstances;

    protected GameObject m_objectPrefab;
    public readonly List<GameObject> m_ActiveObjects = new();
    private readonly Queue<GameObject> m_poolQueue = new();

    protected virtual void Start() {
        SetUpInstances();
    }

    private void SetUpInstances() {
        for(int i = 0; i < m_numberOfInstances; i++) {
            GameObject instance = Instantiate(m_objectPrefab);
            m_poolQueue.Enqueue(instance);
            instance.SetActive(false);
        }
    }

    public GameObject PoolObject(Vector3 position, Quaternion rotation) {
        if(m_poolQueue.Count <= 0){ 
            GameObject newObject = Instantiate(m_objectPrefab, position, rotation);
            m_ActiveObjects.Add(newObject);
            return newObject;
        }

        GameObject obj = m_poolQueue.Dequeue();
        m_ActiveObjects.Add(obj);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void ReturnToQueue(GameObject obj) {
        obj.SetActive(false);
        m_ActiveObjects.Remove(obj);
        m_poolQueue.Enqueue(obj);
    }
}