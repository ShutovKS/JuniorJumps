#region

using System;
using UnityEngine.Serialization;

#endregion

namespace Data.Dynamic.MaxPoints
{
    [Serializable]
    public class MaxPoints
    {
        public MaxPoints()
        {
            value = 0;
        }

        public int value;
    }
}