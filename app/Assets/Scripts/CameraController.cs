﻿/*
Code By: Andrew Sha
This code makes the camera follow a 2D entity around.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Movement2D player;

    public bool isFollowing;

    public float xOffSet;
    public float yOffSet;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement2D>();

        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(player.transform.position.x + xOffSet, player.transform.position.y + yOffSet, transform.position.z);
                };
    }
}
