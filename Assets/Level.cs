using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;
    [SerializeField]
    private int speed;

    private PathFollower currentBlock;
    private int currentBlockIndex = 0;
    private GameObject playerRef;

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
        //puts one after the other
        playerRef = GameObject.Find("Player");
        currentBlock = blocks[0].GetComponent<PathFollower>();
        currentBlock.SelectAndBeginPath(playerRef, speed) ;

        onLastPointOfPath += PutNextBlock;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PutNextBlock()
    {
        if(currentBlockIndex < blocks.Length)
        {
            currentBlockIndex++;
            currentBlock = blocks[currentBlockIndex].GetComponent<PathFollower>();
            //lets move the current block to the last point of the selected path of previous block
            currentBlock.transform.position = playerRef.transform.position;
            currentBlock.SelectAndBeginPath(playerRef,speed);
        }
    }

}
