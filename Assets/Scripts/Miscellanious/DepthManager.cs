using UnityEngine;

namespace Miscellanious
{
    /// <summary>
    /// Class used to control depth in 2D environment.
    /// </summary>
    /// <remarks>Needs a SpriteRenderer component</remarks>
    [RequireComponent(typeof(SpriteRenderer))]
    public class DepthManager : MonoBehaviour
    {
        /// <summary>
        /// Does this depth manager updates itself?
        /// </summary>
        [SerializeField]
        private bool update;

        /// <summary>
        /// Targeted transform object.
        /// </summary>
        [SerializeField]
        private Transform target;


        /// <summary>
        /// The sprite renderer component that will be update.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Main camera used to display.
        /// </summary>
        private Camera _mainCamera;



        /// <summary>
        /// Awake method, used at first to initialize.
        /// </summary>
        private void Start()
        {
            if (target == null)
                target = transform;

            _mainCamera = Camera.current;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        /// <summary>
        /// Fixed Update is called 50 times a second.
        /// </summary>
        private void FixedUpdate()
        {
            if (update)
                _spriteRenderer.sortingOrder = (int)_mainCamera.WorldToScreenPoint(target.position).y * -1;
        }
    }
}