using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GestionBanque.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GestionBanque.Tests
{
    public class ClientTests
    {
        public ClientTests()
        {

        }

        [Fact]
        public void Setter_Nom_ShouldBeValid()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "test@gmail.com");
            var nomAttendu = "Doe";

            // Exécution
            var nomActuel = client.Nom;

            // Affirmation
            Assert.Equal(nomAttendu, nomActuel);
        }

        [Fact]
        public void Setter_Nom_ShouldTrimValue()
        {
            // Préparation
            var client = new Client(1, "  Doe  ", "John", "email@email.com");
            var nomAttendu = "Doe";
            // Exécution
            var nomActuel = client.Nom;
            // Affirmation
            Assert.Equal(nomAttendu, nomActuel);
        }

        [Fact]
        public void Setter_Nom_ShoudThrowArgumentException_WhenValueIsEmpty()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "email@email.com");
            // Exécution & Affirmation
            Assert.Throws<ArgumentException>(() => client.Nom = "");
        }

        [Fact]
        public void Setter_Prenom_ShoudThrowArgumentException_WhenValueIsEmpty()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "abc@email.com");
            // Exécution & Affirmation
            Assert.Throws<ArgumentException>(() => client.Prenom = "");
        }

        [Fact]
        public void Setter_Prenom_ShouldBeValid()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "test@gmail.com");
            var prenomAttendu = "John";

            // Exécution
            var prenomActuel = client.Prenom;

            // Affirmation
            Assert.Equal(prenomAttendu, prenomActuel);
        }

        [Fact]
        public void Setter_Email_ShouldBeValid()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "test@gmail.com");
            var courrielAttendu = "test@gmail.com";

            // Exécution
            var courrielActuel = client.Courriel;

            // Affirmation
            Assert.Equal(courrielAttendu, courrielActuel);
        }

        [Fact]
        public void Setter_Email_ShoudThrowArgumentException_WhenValueIsEmpty()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "email@email.com");
            // Exécution & Affirmation
            Assert.Throws<ArgumentException>(() => client.Courriel = "");
        }

        [Fact]
        public void Setter_Email_ShoudThrowArgumentException_WhenValueIsInvalid()
        {
            // Préparation
            var client = new Client(1, "Doe", "John", "email@email.com");
            // Exécution & Affirmation
            Assert.Throws<ArgumentException>(() => client.Courriel = "invalid-email.com");

        }

    }
}
