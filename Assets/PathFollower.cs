using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private LevelPath[] paths;
    private int selectedIndex;
    private LevelPath selectedPath;
    private GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        paths = GetComponentsInChildren<LevelPath>();
        var rn = new System.Random();
        int selectedIndex = rn.Next(paths.Length);
        Debug.Log("Index: " + selectedIndex);
        selectedPath = paths[selectedIndex];

        //tmp
        playerRef = GameObject.Find("Player");
        BeginSelectedPath();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginSelectedPath()
    {
        selectedPath.BeginPath(playerRef);
    }


}
