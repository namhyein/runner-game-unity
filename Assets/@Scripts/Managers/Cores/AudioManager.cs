using System;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.ESound.Max];
    private Dictionary<string, AudioClip> _audioClips = new();
    private GameObject _soundRoot = null;

    /* 사운드 루트를 찾거나 생성 */
    public void Init()
    {
        if (_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");

            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                UnityEngine.Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = Enum.GetNames(typeof(Define.ESound));
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new() { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Define.ESound.Bgm].loop = true;
            }
        }
    }

    /* 모든 사운드를 정지하고 사운드 클립을 제거 */
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();

        _audioClips.Clear();
    }

    /* 특정 타입(bgm, sfx)의 사운드를 재생 */
    public void Play(Define.ESound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Play();
    }

    /* 특정 타입(bgm, sfx)의 사운드를 재생 - 오디오클립으로 로드 후 재생 */
    public void Play(Define.ESound type, string key, float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == Define.ESound.Bgm)
        {
            LoadAudioClip(key, (audioClip) =>
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.clip = audioClip;
                audioSource.Play();
            });
        }
        else
        {
            LoadAudioClip(key, (audioClip) =>
            {
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            });
        }
    }

    /* 특정 타입(bgm, sfx)의 사운드를 재생 - 오디오클립으로 로드되어 있는 것을 재생 */
    public void Play(Define.ESound type, AudioClip audioClip, float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == Define.ESound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    /* 특정 타입(bgm, sfx)의 사운드 재생을 중단 */
    public void Stop(Define.ESound type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    /* key에 해당하는 사운드 클립을 로드 */
    private void LoadAudioClip(string key, Action<AudioClip> callback)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(key, out audioClip))
        {
            callback?.Invoke(audioClip);
            return;
        }

        audioClip = Managers.Resource.Load<AudioClip>(key);

        if (_audioClips.ContainsKey(key) == false)
            _audioClips.Add(key, audioClip);

        callback?.Invoke(audioClip);
    }
}
