using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EmmiterController : MonoBehaviour {
    AudioSource m_audioSource;
    Sound m_sound;

    private void Awake() {
        m_audioSource = GetComponent<AudioSource>();
    }
	
    void Update(){
        if(m_sound == null) return;
        if(!m_audioSource.isPlaying){
            m_sound = null;
            SoundPool.Instance.ReturnEmmiter(this);
        }
    }

    public void SetupSound(Sound sound, float volume) {
        m_sound 				= sound;
        m_audioSource.clip 		= m_sound.clip;
        m_audioSource.volume 	= m_sound.volume * volume;
        m_audioSource.pitch 	= m_sound.pitch;
        m_audioSource.loop 		= m_sound.loop;
    }

    public void SetupSound3D(Sound sound, float volume) {
        SetupSound(sound, volume);
        m_audioSource.spatialBlend = 1;
        m_audioSource.spatialize = true;
    }

    public void PlaySound() {
        m_audioSource.Play();
    }

    public void StopSound() {
        m_audioSource.Stop();
    }

    public void UpdateVolume(float volumen){
        m_audioSource.volume = m_sound.volume * volumen;
    }
}