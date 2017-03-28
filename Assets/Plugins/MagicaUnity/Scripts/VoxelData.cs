using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GN00T.MagicaUnity
{
    /// <summary>
    /// Voxel data class contains color indices as bytes
    /// </summary>
    [System.Serializable]
    public class VoxelData
    {

        [SerializeField]
        private int _voxelsWide, _voxelsTall, _voxelsDeep;
        [SerializeField]
        private byte[] colors;
        /// <summary>
        /// Creates a new empty voxel data
        /// </summary>
        public VoxelData()
        {
            
        }

        /// <summary>
        /// Creates a voxeldata with provided dimensions
        /// </summary>
        /// <param name="voxelsWide"></param>
        /// <param name="voxelsTall"></param>
        /// <param name="voxelsDeep"></param>
        public VoxelData(int voxelsWide, int voxelsTall, int voxelsDeep)
        {
            Resize(voxelsWide, voxelsTall, voxelsDeep);
        }

        public void Resize(int voxelsWide, int voxelsTall, int voxelsDeep)
        {
            this._voxelsWide = voxelsWide;
            this._voxelsTall = voxelsTall;
            this._voxelsDeep = voxelsDeep;
            colors = new byte[_voxelsWide * _voxelsTall * _voxelsDeep];
        }
        /// <summary>
        /// Gets a grid position from voxel coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public int GetGridPos(int x, int y, int z)
        {
            return (_voxelsWide * _voxelsTall) * z + (_voxelsWide * y) + x;
        }

        /// <summary>
        /// Sets a color index from voxel coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="value"></param>
        public void Set(int x, int y, int z, byte value)
        {
            colors[GetGridPos(x, y, z)] = value;
        }
        /// <summary>
        /// Sets a color index from grid position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="value"></param>
        public void Set(int x, byte value)
        {
            colors[x] = value;
        }
        /// <summary>
        /// Gets a pallete index from voxel coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public int Get(int x, int y, int z)
        {
            return colors[GetGridPos(x, y, z)];
        }
        /// <summary>
        /// Gets a pallete index from a grid position
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public byte Get(int x)
        {
            return colors[x];
        }

        /// <summary>
        /// width of the data in voxels
        /// </summary>
        public int VoxelsWide
        {
            get { return _voxelsWide; }
        }

        /// <summary>
        /// height of the data in voxels
        /// </summary>
        public int VoxelsTall
        {
            get { return _voxelsTall; }
        }
        /// <summary>
        /// Depth of the voxels in data
        /// </summary>
        public int VoxelsDeep
        {
            get { return _voxelsDeep; }
        }

        /// <summary>
        /// Voxel dimension as integers
        /// </summary>
        public Vector3 VoxelDimension
        {
            get { return new Vector3(_voxelsWide, _voxelsTall, _voxelsDeep); }
        }

    }
}
