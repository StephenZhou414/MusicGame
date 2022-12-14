using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private LevelPath[] paths;
    private int selectedIndex;
    private LevelPath selectedPath;
    [SerializeField]
    private AudioClip[] audios;

    private AudioClip selectedClip;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAndBeginPath(GameObject player, int speed)
    {
        //paint all possibilities
        paths = GetComponentsInChildren<LevelPath>();
        foreach (var lvlPath in paths)
        {
            lvlPath.PaintPath();
        }
        //Select one
        var rn = new System.Random();
        int selectedIndex = rn.Next(paths.Length);
        selectedPath = paths[selectedIndex];
        selectedClip = audios[selectedIndex];
        AudioSource.PlayClipAtPoint(selectedClip, Camera.main.transform.position);
        //Move through that one
        selectedPath.BeginPath(player,speed);
    }

    public Transform GetLastPoint()
    {
        return selectedPath.GetLastPoint();
    }

    public AudioClip GetSelectedClip()
    {
        return selectedClip;
    }



}
