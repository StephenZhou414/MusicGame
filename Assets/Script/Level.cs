using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;
    [SerializeField]
    private int speed, turns;
    [SerializeField]
    private UIController uicontrol;
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    private List<AudioClip> music = new List<AudioClip>();

    private PathFollower currentBlock;
    private int currentBlockIndex = 0;
    private GameObject playerRef;
    private int currentTurn = 1;
    private bool gameover = false;

    public static Level current;
    private void Awake()
    {
        current = this;
    }
    public event Action onLastPointOfPath;
    public void LastPointOfPath()
    {
        if (onLastPointOfPath != null)
            onLastPointOfPath();
    }


    // Start is called before the first frame update
    void Start()
    {

        //an instance of the first block is always the first one to start
        playerRef = GameObject.Find("Player");
        PlaceNextBlock(0);
        //currentBlock = blocks[0].GetComponent<PathFollower>();
        //currentBlock.SelectAndBeginPath(playerRef, speed) ;

        onLastPointOfPath += EnableControls;
    }

    private void Update()
    {
        if(gameover && vcam.m_Lens.OrthographicSize < 15)
        {
            vcam.m_Lens.OrthographicSize += Time.deltaTime;
        }
    }

    private void EnableControls()
    {
        if(currentTurn <= turns)
        {
            uicontrol.ToggleShowUI();
        }
        else
        {
            //done. Let's play the music
            gameover = true;
            StartCoroutine(PlayMusic());

        }
    }

    public void PlaceNextBlock(int option)
    {
        GameObject nextBlock = Instantiate(blocks[option], playerRef.transform.position, playerRef.transform.rotation);
        currentBlock = nextBlock.GetComponent<PathFollower>();
        //lets move the current block to the last point of the selected path of previous block
        currentBlock.transform.position = playerRef.transform.position;
        currentBlock.SelectAndBeginPath(playerRef, speed);
        music.Add(currentBlock.GetSelectedClip());
        currentTurn++;
    }

    IEnumerator PlayMusic()
    {
        foreach (AudioClip clip in music)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            yield return new WaitForSeconds(clip.length / 10);

        }
    }



}
