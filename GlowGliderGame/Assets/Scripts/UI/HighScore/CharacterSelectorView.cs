using UnityEngine;
using UnityEngine.UI;

namespace UI.HighScore
{
    public class CharacterSelectorView : MonoBehaviour
    {
        public Button UpButton;
        public Button DownButton;
        public Text CharacterText;

        public ICharacterSelectorViewModel ViewModel { get; set; }

        void Start()
        {
            UpButton.onClick.AddListener(ViewModel.IncrementCharacter);
            DownButton.onClick.AddListener(ViewModel.DecrementCharacter);
        }

        // Update is called once per frame
        void Update()
        {
            CharacterText.text = ViewModel.Character;
        }
    }
}
