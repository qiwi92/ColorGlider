using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.HighScore
{
    public class NameInputView : MonoBehaviour
    {
        public GameObject SelectorPrefab;

        private readonly List<CharacterSelectorViewModel> _viewModels = new List<CharacterSelectorViewModel>();

        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                var viewModel = new CharacterSelectorViewModel();
                var view = Instantiate(SelectorPrefab, transform).GetComponent<CharacterSelectorView>();
                view.ViewModel = viewModel;

                _viewModels.Add(viewModel);
            }
        }

        public string GetEnteredName()
        {
            return string.Join(string.Empty, _viewModels.Select(vm => vm.Character));
        }
    }
}