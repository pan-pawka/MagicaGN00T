using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GN00T.MagicaUnity
{
    [CreateAssetMenu(fileName = "VoxModel", menuName = "Voxel/Model", order = 0)]
    public class VoxModel : ScriptableObject
    {

        public Color[] pallete;
        public List<VoxelData> frames = new List<VoxelData>();
        public List<Mesh> meshes = new List<Mesh>();
        public float modelScale = 1f;

    }
}
