﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HighScore
{
    public sealed class NameInputView : MonoBehaviour
    {
        public GameObject SelectorPrefab;
        [SerializeField] private Button _submitButton;
        [SerializeField] private Transform _characterParent;
        private readonly List<CharacterSelectorViewModel> _viewModels = new List<CharacterSelectorViewModel>();
        public event Action<string> SubmitRequested;

        public Text NewPlayerHighScoreText;

        public void Initialize()
        {
            for (int i = 0; i < 5; i++)
            {
                var viewModel = new CharacterSelectorViewModel();
                var view = Instantiate(SelectorPrefab, _characterParent).GetComponent<CharacterSelectorView>();
                view.ViewModel = viewModel;

                _viewModels.Add(viewModel);
            }

            _submitButton.onClick.AddListener(OnSubmitRequested);

            gameObject.SetActive(false);
        }

        public void Open()
        {
            var alias = SavegameService.Instance.Alias;

            if (!string.IsNullOrEmpty(alias))
            {
                for (var index = 0; index < _viewModels.Count; index++)
                {
                    var aliasChar = index < alias.Length ? alias[index].ToString() : "-";
                    _viewModels[index].Character = aliasChar;
                }
            }

            gameObject.SetActive(true);
        }

        private void Update()
        {
            _submitButton.interactable = !string.IsNullOrWhiteSpace(GetEnteredName());
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnSubmitRequested()
        {
            var enteredName = GetEnteredName();
            SavegameService.Instance.Alias = enteredName;

            SubmitRequested?.Invoke(enteredName);
        }

        private string GetEnteredName()
        {
            return string.Join(string.Empty, _viewModels.Select(vm => vm.Character)).Trim('-');
        }
    }
}