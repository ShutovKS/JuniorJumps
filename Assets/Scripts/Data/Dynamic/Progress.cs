#region

using System;

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