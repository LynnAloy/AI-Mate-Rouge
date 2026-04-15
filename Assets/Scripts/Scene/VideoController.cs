using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : Singleton<VideoController>
{
    [SerializeField] private VideoPlayer videoPlayer;

    public Action OnVideoStop;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);
            OnVideoStop?.Invoke();
        }
    }
}
