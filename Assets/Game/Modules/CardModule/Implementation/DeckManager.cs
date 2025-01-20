using System.Collections.Generic;
using Game.Modules.CardModule.Declaration;
using ObservableCollections;
using UnityEngine;

namespace Game.Modules.CardModule.Implementation
{
    public class DeckManager : IDeckManager
    {
        public ObservableList<CardEffect> DrawPile { get; }
        public ObservableList<CardEffect> DiscardPile { get; }

        private readonly List<CardEffect> _availableEffects;
        private readonly int _initialDeckSize;

        public DeckManager(List<CardEffect> availableEffects, int initialDeckSize)
        {
            _availableEffects = availableEffects;
            _initialDeckSize = initialDeckSize;

            DrawPile = new ObservableList<CardEffect>(GenerateRandomDeck(initialDeckSize));
            DiscardPile = new ObservableList<CardEffect>();
            Shuffle(DrawPile);
        }

        public List<CardEffect> DrawCards(int count)
        {
            var drawnCards = new List<CardEffect>();

            for (var index = 0; index < count; index++)
            {
                if (DrawPile.Count == 0)
                {
                    ReshuffleDiscardPile();
                    if (DrawPile.Count == 0)
                    {
                        break;
                    }
                }

                var card = DrawPile[0];
                DrawPile.RemoveAt(0);
                drawnCards.Add(card);
            }

            return drawnCards;
        }

        public void AddToDiscardPile(CardEffect card)
        {
            DiscardPile.Add(card);
        }

        private void ReshuffleDiscardPile()
        {
            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();

            while (DrawPile.Count < _initialDeckSize)
            {
                var randomEffect = _availableEffects[Random.Range(0, _availableEffects.Count)];
                DrawPile.Add(randomEffect);
            }

            Shuffle(DrawPile);
        }

        private void Shuffle(ObservableList<CardEffect> deck)
        {
            for (var index = 0; index < deck.Count; index++)
            {
                var temp = deck[index];
                var randomIndex = Random.Range(index, deck.Count);
                deck[index] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }

        private List<CardEffect> GenerateRandomDeck(int size)
        {
            var deck = new List<CardEffect>();
            for (var index = 0; index < size; index++)
            {
                var randomEffect = _availableEffects[Random.Range(0, _availableEffects.Count)];
                deck.Add(randomEffect);
            }

            return deck;
        }
    }
}