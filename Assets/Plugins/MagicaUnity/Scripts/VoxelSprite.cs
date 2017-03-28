using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GN00T.MagicaUnity
{
    /// <summary>
    /// Static non animated voxel sprite
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class VoxelSprite : MonoBehaviour
    {
        [Header("Voxel model and animation for this sprite")]
        public VoxModel model;
        protected MeshFilter _meshFilter;

        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            if(model != null)
                _meshFilter.sharedMesh = model.meshes[0];
        }

        void OnDrawGizmos()
        {
           
        }
    }
}
