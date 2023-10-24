using Enemies.Enemy_Types;

namespace UI.Library
{
    /// <summary>
    /// Class used to handle enemy info in library.
    /// </summary>
    public class EnemyInfo : ObjectInfo
    {
        /// <summary>
        /// Enemy data buffered.
        /// </summary>
        private Enemy _enemy;
        public Enemy Enemy
        {
            get => _enemy;

            set
            {
                _enemy = value;
                screenShot.sprite = _enemy.ScreenShot;
            }
        }
    }
}