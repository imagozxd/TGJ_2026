using UnityEngine;

class SoundPool : PoolSystem<SoundPool> {
	
    [SerializeField] GameObject m_soundEmmiterPrefab;

    protected override void Start() {
        m_objectPrefab = m_soundEmmiterPrefab;
        base.Start();
    }

    public EmmiterController PoolSoundEmmiter() {
        var emmiterObject = PoolObject(Vector3.zero, Quaternion.identity);
        EmmiterController emmiter = emmiterObject.GetComponent<EmmiterController>();
        return emmiter;
    }

    public EmmiterController PoolSoundEmmiterInPosition(Vector3 position) {
        var emmiterObject = PoolSoundEmmiter();
        emmiterObject.transform.position = position;
        return emmiterObject;
    }

    public void ReturnEmmiter(EmmiterController emmiter) {
        ReturnToQueue(emmiter.gameObject);
    }

}