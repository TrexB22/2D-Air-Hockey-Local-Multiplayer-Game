using UnityEngine;
using UnityEngine.UI;

namespace AirHockey.UI.Generic
{
    public class GenericToggleGroup<T,TU> : ToggleGroup where T : GenericToggle<TU>
    {
        #region Serialized fields

        [SerializeField] private T[] _toggles;

        #endregion

        #region Properties
        
        public TU Selected { get; private set; }

        #endregion

        #region Setup

        protected override void Awake()
        {
            foreach (var toggle in _toggles)
            {
                toggle.Deselect();
                toggle.OnSelect += SelectToggle;
            }
            _toggles[0].Select();
        }
        
        protected override void OnDestroy()
        {
            foreach (var toggle in _toggles)
            {
                toggle.OnSelect -= SelectToggle;
            }
        }

        protected override void Reset()
        {
            _toggles = GetComponentsInChildren<T>();
        }

        #endregion

        #region Event handlers

        private void SelectToggle(GenericToggle<TU> selectedToggle)
        {
            foreach (var toggle in _toggles)
            {
                if (toggle != selectedToggle)
                    toggle.Deselect();
            }

            Selected = selectedToggle.Value;
        }

        #endregion
    }
}