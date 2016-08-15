using System;
using System.Collections.Generic;
using NUnit.Framework;
using CodingExercise;
using System.Linq;

namespace Tests
{
    public class Tests
    {

        [Test]
        public void should_popluate_deck_of_cards()
        {
            var deck = new Deck();
            
            //verify that number of cards should be 52
            Assert.AreEqual(52, deck.Cards.Count);

            //verify that the deck of cards has no duplicate card
            var hasDuplicate = deck.Cards.GroupBy(c => new { c.Rank, c.Suit }).Any(c1 => c1.Count() > 1);
            Assert.AreEqual(false, hasDuplicate);
        }

        [Test]
        public void should_throw_exception_when_suit_is_invalid()
        {
            Assert.Throws(Is.TypeOf<KeyNotFoundException>()
                            .And.Message.Contains("Invalid suit:")
                            , () => new Card("invalid", "Two"));
        }

        [Test]
        public void should_throw_exception_when_rank_is_invalid()
        {
            Assert.Throws(Is.TypeOf<KeyNotFoundException>()
                            .And.Message.Contains("Invalid rank:")
                            , () => new Card("Space", "invalid"));
        }

        [Test]
        public void should_sort_the_deck_of_cards_by_ascending_order()
        {
            var deck = new Deck();
            var sortedDeckOfCards = deck.Sort(deck.Cards);

            //verify the list is sorted
            var ordered = sortedDeckOfCards.Zip(sortedDeckOfCards.Skip(1), (a, b) => new { a, b })
                                    .All(p => p.a.Point < p.b.Point);
            Assert.AreEqual(true, ordered);

            //verify there is still 52 cards in the deck
            Assert.AreEqual(52, sortedDeckOfCards.Count);
        }

        [Test]
        public void should_shuffle_the_deck_of_the_cards()
        {
            var deck = new Deck();

            //shuffle the deck by swapping the card 20 times
            var shuffledCards = deck.Shuffle(deck.Cards, 20);

            //verify that the deck is no longer sorted.
            var ordered = shuffledCards.Zip(shuffledCards.Skip(1), (a, b) => new { a, b })
                        .All(p => p.a.Point < p.b.Point);
            Assert.AreEqual(false, ordered);

            //verify there is no duplicated card
            var hasDuplicate = shuffledCards.GroupBy(c => new { c.Rank, c.Suit }).Any(c1 => c1.Count() > 1);
            Assert.AreEqual(false, hasDuplicate);

            //verify there is still 52 cards in the deck
            Assert.AreEqual(52, shuffledCards.Count);
        }

        [Test]
        public void should_throw_exception_when_shuffling_a_null_deck_of_card()
        {
            var deck = new Deck();
            Assert.Throws(Is.TypeOf<ArgumentNullException>()
                .And.Message.Contains("Deck of cards is null")
                , () => deck.Shuffle(null));
        }

        [Test]
        public void should_throw_exception_when_number_of_swap_is_less_than_one()
        {
            var deck = new Deck();
            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.Contains("Number of swap should be equal or greater than 1")
                , () => deck.Shuffle(deck.Cards, -1));
        }
    }
}
