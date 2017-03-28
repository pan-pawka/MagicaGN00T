using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GN00T.MagicaUnity {
    [CreateAssetMenu(fileName ="Voxel Animation",menuName ="Voxel/Animation",order =0)]
    public class VoxelAnimation :ScriptableObject{
        public VoxModel targetData;
        public int startFrame = 0;
        public int endFrame = 0;
        public float runTime = 1f;
        public bool looping = false;
        public string animName = "Default";
    }
}
