using System;

namespace Save
{
    /// <summary>
    /// Class used to save one level progression.
    /// </summary>
    [Serializable]
    public class LevelSave
    {
        /// <summary>
        /// Number of lives lost on this level.
        /// </summary>
        public int SeedsGained { get; set; }

        /// <summary>
        /// Actual state of the level.
        /// </summary>
        public LevelState State { get; set; }



        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="newSeedsGained">Number of seeds gained (related to lives lost)</param>
        /// <param name="newLevelState">New state</param>
        public LevelSave(int newSeedsGained, LevelState newLevelState)
        {
            SeedsGained = newSeedsGained;
            State = newLevelState;
        }
    }
}