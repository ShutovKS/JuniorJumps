#region

using System;
using UnityEngine.Serialization;

#endregion

namespace Data.Dynamic
{
    [Serializable]
    public class Progress
    {
        public Progress()
        {
            maxPoints = new MaxPoints.MaxPoints();
        }

        public MaxPoints.MaxPoints maxPoints;
    }
}