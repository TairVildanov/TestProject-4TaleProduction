using Game.Modules.CardModule.Declaration;
using ObservableCollections;
using R3;
using TMPro;
using UnityEngine;

namespace Game.Modules.CardModule.Presentation
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _drawPileText;
        [SerializeField] private TextMeshProUGUI _discardPileText;

        private IDeckManager _deckManager;

        public void Initialize(IDeckManager deckManager)
        {
            _deckManager = deckManager;
            _deckManager.DiscardPile.ObserveChanged().Subscribe(_ => UpdateDiscardPile());
            _deckManager.DrawPile.ObserveChanged().Subscribe(_ => UpdateDrawPile());
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateDrawPile();
            UpdateDiscardPile();
        }
        private void UpdateDrawPile()
        {
            _drawPileText.text = $"{_deckManager.DrawPile.Count}";
        }

        private void UpdateDiscardPile()
        {
            _discardPileText.text = $"{_deckManager.DiscardPile.Count}";
        }
    }
}