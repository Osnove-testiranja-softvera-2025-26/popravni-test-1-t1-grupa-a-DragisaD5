using NUnit.Framework;
using OTS2026_PT1_GrupaA;
using OTS2026_PT1_GrupaA.Exceptions;
using OTS2026_PT1_GrupaA.Models;
using System;

namespace OTS2026_PT1_GrupaA.Test
{
    [TestFixture]
    internal class GameTest
    {

        // F1

        [Test]
        public void PlayerPositionOutsideLandOnGameCreation_ThrowsInvalidPlayerPositionException()
        {
            Position invalidPlayerPos = new Position(0, 0);
            Position validBoatPos = new Position(5, 10);

            Exception ex = Assert.Throws<InvalidPlayerPositionException>((TestDelegate)(() => new Game(invalidPlayerPos, validBoatPos)));
            Assert.That(ex.Message, Is.EqualTo("Player and boat must be in the Land zone!"));
        }

        [Test]
        public void BoatPositionOutsideLandOnGameCreation_ThrowsInvalidPlayerPositionException()
        {
            Position validPlayerPos = new Position(5, 10);
            Position invalidBoatPos = new Position(0, 0);

            Exception ex = Assert.Throws<InvalidPlayerPositionException>((TestDelegate)(() => new Game(validPlayerPos, invalidBoatPos)));
            Assert.That(ex.Message, Is.EqualTo("Player and boat must be in the Land zone!"));
        }

        private Game game;

        [SetUp]
        public void SetUp()
        {

            game = new Game(new Position(5, 10), new Position(6, 10));
        }

        // F2 & F3
        
        [TestCase(5, 10, 11)]
        [TestCase(5, 11, 12)]
        [TestCase(5, 12, 13)]
        [TestCase(5, 13, 14)]
        [TestCase(5, 14, 15)]
        [TestCase(5, 15, 16)]
        [TestCase(5, 16, 17)]
        [TestCase(5, 17, 18)]
        [TestCase(5, 18, 19)]
        [TestCase(5, 19, 20)]
        [TestCase(5, 20, 21)]
        [TestCase(5, 21, 22)]
        [TestCase(5, 22, 23)]
        [TestCase(5, 23, 24)]
        [TestCase(5, 24, 25)]
        public void SuccessfullPlayerMoveDown_InsideLandZone(int startX, int startY, int expectedY)
        {
            game.Player.Position = new Position(startX, startY);
            game.MovePlayer(Move.Down);

            Assert.That(game.Player.Position.Y, Is.EqualTo(expectedY));
        }

        [TestCase(15, 19, 19)] 
        public void PlayerMoveToInvalidZone_ShouldNotChangePosition(int startX, int startY, int expectedY)
        {
            game.Player.Position = new Position(startX, startY);
            game.MovePlayer(Move.Down);

            Assert.That(game.Player.Position.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void PlayerMoveToPondWithoutBoat_ShouldNotMove()
        {
            game.Player.Position = new Position(19, 15);
            game.Player.HasBoat = false; 

            game.MovePlayer(Move.Right);

            Assert.That(game.Player.Position.X, Is.EqualTo(19));
        }

        [Test]
        public void PlayerMoveOnLand_CollectsBaitAndEmptiesTile()
        {
            game.Player.Position = new Position(5, 10);
            game.Player.AmountOfBait = 0;
            game.Map.Fields[5, 10].Content = FieldContent.Bait;

            game.ResolvePlayerPosition();

            Assert.That(game.Player.AmountOfBait, Is.EqualTo(1));
            Assert.That(game.Map.Fields[5, 10].Content, Is.EqualTo(FieldContent.Empty));
        }

        [Test]
        public void PlayerWithBait_CatchesFishSuccessfully()
        {
            game.Player.Position = new Position(25, 10);
            game.Player.HasBoat = true;
            game.Player.AmountOfBait = 1;
            game.Player.AmountOfFish = 0;
            game.Map.Fields[25, 10].Content = FieldContent.Fish;

            game.ResolvePlayerPosition();

            Assert.That(game.Player.AmountOfFish, Is.EqualTo(1));
            Assert.That(game.Player.AmountOfBait, Is.EqualTo(0));
            Assert.That(game.Map.Fields[25, 10].Content, Is.EqualTo(FieldContent.Empty));
        }

        // F4

        [TestCase(13, 0, false, Game.Score.Good)]      
        [TestCase(5, 10, true, Game.Score.Average)]    
        [TestCase(8, 10, true, Game.Score.Good)]
        [TestCase(2, 5, true, Game.Score.Bad)]
        [TestCase(0, 15, false, Game.Score.Bad)]     
        public void CalculateIncome_CombinatorialTests(int fish, int bait, bool hasBoat, Game.Score expectedScore)
        {
            game.Player.AmountOfFish = fish;
            game.Player.AmountOfBait = bait;
            game.Player.HasBoat = hasBoat;

            Game.Score actualScore = game.CalculateIncome();

            Assert.That(actualScore, Is.EqualTo(expectedScore));
        }
    }
}