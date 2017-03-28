using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GN00T.MagicaUnity
{
    [CreateAssetMenu(fileName = "VoxModel", menuName = "Voxel/Model", order = 0)]
    public class VoxModel : ScriptableObject
    {
        [Header("Model color pallete")]
        public Color[] pallete;
        [Header("Mesh Frame data")]
        public List<VoxelData> meshData = new List<VoxelData>();
        [Header("Meshes attached to model")]
        public List<Mesh> meshes = new List<Mesh>();
        [Header("Scale for Voxel to world coordinates")]
        public float modelScale = 1f;
        [Header("Origin of model scale .5 center 0 left 1 right for each axis")]
        public Vector3 origin = new Vector3(.5f, .5f, .5f);
        
    }
}
