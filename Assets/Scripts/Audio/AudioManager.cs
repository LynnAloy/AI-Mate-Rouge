using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioType[] AudioTypes;
    public AudioMixer mixer;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        foreach (var type in AudioTypes)
        {
            type.Source = gameObject.AddComponent<AudioSource>();
            type.Source.clip = type.Clip;
            type.Source.name = type.ClipName;
            type.Source.volume = type.Volume;
            type.Source.pitch = type.Pitch;
            type.Source.loop = type.Loop;
            type.Source.playOnAwake = type.PlayOnAwake;
            if (type.MixerGroup != null)
            {
                type.Source.outputAudioMixerGroup = type.MixerGroup;
            }
        }
    }

    private void Start()
    {
        // 启动循环播放协程
        StartCoroutine(BGMCoroutine());
    }

    private IEnumerator BGMCoroutine()
    {
        while (true)
        {
            // 如果当前没有音乐在播放，就随机播放一首
            if (!IsAnyPlaying() && AudioTypes.Length > 0)
            {
                int randomIndex = Random.Range(0, AudioTypes.Length);
                var chosen = AudioTypes[randomIndex];
                chosen.Source.Play();
                Debug.Log($"Playing random BGM: {chosen.ClipName}");

                // 等待这首歌播放完毕（clip.length 秒）
                yield return new WaitForSeconds(chosen.Source.clip.length);
            }
            else
            {
                // 如果正在播放，等一帧再检查
                yield return null;
            }
        }
    }

    private bool IsAnyPlaying()
    {
        foreach (var type in AudioTypes)
        {
            if (type.Source.isPlaying) return true;
        }
        return false;
    }
}
