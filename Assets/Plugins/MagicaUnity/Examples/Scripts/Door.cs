using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GN00T.MagicaUnity;
[RequireComponent(typeof(AnimatedVoxelSprite))]
public class Door : MonoBehaviour {
    private AnimatedVoxelSprite _voxelSprite;
    public VoxelAnimation openAnim, closeAnim;
    public bool open=true;
	// Use this for initialization
	void Start () {
        _voxelSprite = GetComponent<AnimatedVoxelSprite>();
        _voxelSprite.SetAnimation(openAnim);
	}
	
	// Update is called once per frame
	void Update () {
        if (_voxelSprite.CurrentAnimationCompleted)
        {
            open = !open;
            _voxelSprite.SetAnimation(open ? openAnim : closeAnim);
        }
	}
}
