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
            MaxPoints = new MaxPoints.MaxPoints();
        }

        public MaxPoints.MaxPoints MaxPoints { get; set; }
    }
}