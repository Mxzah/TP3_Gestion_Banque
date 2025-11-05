using System;
using GestionBanque.Models;
using GestionBanque.Models.DataService;
using Moq;
using GestionBanque.ViewModels.Interfaces;

namespace GestionBanque.Tests
{
    public class CompteTests
    {

        public CompteTests()
        {

        }

        [Fact]
        public void Retirer_ShouldBeValid()
        {
            // Préparation
            Compte compte = new Compte(1, "1234", 500.0, 1);
            double montantRetrait = 200.0;
            double balanceAttendue = 300.0;
            // Exécution
            compte.Retirer(montantRetrait);
            // Affirmation
            Assert.Equal(balanceAttendue, compte.Balance);
        }

        [Fact]
        public void Retirer_ShouldThrow_WhenAmountIsNegative()
        {
            var compte = new Compte(1, "1234", 500.0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(-200.0));
            Assert.Equal(500.0, compte.Balance);
        }

        [Fact]
        public void Retirer_ShouldThrow_WhenAmountExceedsBalance()
        {
            var compte = new Compte(1, "1234", 500.0, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(600.0));
            Assert.Equal(500.0, compte.Balance);
        }

        [Fact]
        public void Retirer_ShouldNotChangeBalance_WhenAmountIsZero()
        {
            var compte = new Compte(1, "1234", 500.0, 1);

            compte.Retirer(0.0);

            Assert.Equal(500.0, compte.Balance);
        }

        [Fact]
        public void Deposer_ShouldBeValid()
        {
            // Préparation
            Compte compte = new Compte(1, "1234", 500.0, 1);
            double montantDepot = 200.0;
            double balanceAttendue = 700.0;
            // Exécution
            compte.Deposer(montantDepot);
            // Affirmation
            Assert.Equal(balanceAttendue, compte.Balance);
        }

        [Fact]
        public void Deposer_ShouldThrow_WhenAmountIsNegative()
        {
            var compte = new Compte(1, "1234", 500.0, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Deposer(-200.0));
            Assert.Equal(500.0, compte.Balance);
        }

        [Fact]
        public void Deposer_ShouldNotChangeBalance_WhenAmountIsZero()
        {
            var compte = new Compte(1, "1234", 500.0, 1);
            compte.Deposer(0.0);
            Assert.Equal(500.0, compte.Balance);
        }

    }
}
