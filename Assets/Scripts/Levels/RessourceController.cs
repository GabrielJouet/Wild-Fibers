using TMPro;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class used to store and manage resources (gold and lives).
    /// </summary>
    /// <remarks>Needs a Level Controller component</remarks>
    [RequireComponent(typeof(LevelController))]
    public class RessourceController : MonoBehaviour
    {
        [Header("UI Elements")]

        /// <summary>
        /// Life text component used to display lives count.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI lifeText;

        /// <summary>
        /// Health animator component updated each time we lose health.
        /// </summary>
        [SerializeField]
        private Animator lifeIconAnimator;

        /// <summary>
        /// Gold text component used to display gold count.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI goldText;

        /// <summary>
        /// Gold animator component updated each time we gain money.
        /// </summary>
        [SerializeField]
        private Animator goldIconAnimator;


        /// <summary>
        /// Gold count in game.
        /// </summary>
        public int GoldCount { get; private set; }

        /// <summary>
        /// Lives lost in game.
        /// </summary>
        public int LivesLost => _lifeCountMax - _lifeCount;


        /// <summary>
        /// Life count max and life count.
        /// </summary>
        private int _lifeCountMax;
        private int _lifeCount;

        /// <summary>
        /// Level controller component used to retrieve lives and gold count.
        /// </summary>
        private LevelController _levelController;

        private static readonly int LoseAnimation = Animator.StringToHash("lose");


        /// <summary>
        /// Start method, called after Start.
        /// </summary>
        private void Start()
        {
            _levelController = GetComponent<LevelController>();

            _lifeCountMax = _levelController.LoadedLevel.LifeCount + Controller.Instance.SquadController.CurrentSquad.LivesBonus;
            _lifeCount = _lifeCountMax;

            GoldCount = _levelController.LoadedLevel.GoldCount + Controller.Instance.SquadController.CurrentSquad.GoldBonus;
            goldText.text = GoldCount.ToString();
            lifeText.text = _lifeCount.ToString();
        }



        #region Gold related
        /// <summary>
        /// Add gold to count.
        /// </summary>
        /// <param name="count">Gold count added</param>
        /// <param name="applyMultiplier">Does the gold multiplier apply here?</param>
        public void AddGold(int count, bool applyMultiplier)
        {
            if (count > 0f)
            {
                goldIconAnimator.SetTrigger(LoseAnimation);

                //If we are in a special level the amount of gold gained is factored.
                GoldCount += Mathf.RoundToInt(count * (applyMultiplier ? _levelController.LoadedLevel.GoldMultiplier : 1));
                goldText.text = GoldCount.ToString();
            }
        }


        /// <summary>
        /// Remove gold to count.
        /// </summary>
        /// <param name="count">Gold count removed</param>
        public void RemoveGold(int count)
        {
            GoldCount -= count;
            goldText.text = GoldCount.ToString();
        }
        #endregion



        #region Lives related
        /// <summary>
        /// Removes lives to count and display death if lives is below 0.
        /// </summary>
        /// <param name="count">Gold count removed</param>
        public void RemoveLives(int count)
        {
            if (_lifeCount > 0)
            {
                lifeIconAnimator.SetTrigger(LoseAnimation);

                if (_lifeCount - count <= 0)
                {
                    _lifeCount = 0;
                    _levelController.EndLevel(true);
                }
                else
                    _lifeCount -= count;

                lifeText.text = _lifeCount.ToString();
            }
        }
        #endregion
    }
}