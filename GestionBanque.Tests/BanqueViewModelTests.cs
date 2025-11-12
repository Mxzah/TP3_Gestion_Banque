using GestionBanque.Models;
using GestionBanque.Models.DataService;
using GestionBanque.ViewModels;
using GestionBanque.ViewModels.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBanque.Tests
{
    public class BanqueViewModelTests
    {
        private readonly Mock<IInteractionUtilisateur> _dialServiceMock = new Mock<IInteractionUtilisateur>();
        private readonly Mock<IDataService<Client>> _pDataServiceMockClient = new Mock<IDataService<Client>>();
        private readonly Mock<IDataService<Compte>> _pDataServiceMockCompte = new Mock<IDataService<Compte>>();

        public BanqueViewModelTests()
        {
            _dialServiceMock.Setup(ds => ds.AfficherMessageErreur(It.IsAny<string>()));
            _dialServiceMock.Setup(ds => ds.PoserQuestion(It.IsAny<string>())).Returns(true);
        }

        [Fact]
        public void Constructeur_ShouldInitializeProperties()
        {
            // Arrange & Act
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            // Assert
            Assert.NotNull(viewModel.Clients);
            Assert.Null(viewModel.ClientSelectionne);
            Assert.Null(viewModel.CompteSelectionne);
        }

        [Fact]
        public void ClientSelectionne_Setter_ShouldUpdateProperties()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Client client = new Client(1, "Doe", "John", "email@email.com");

            // Act
            viewModel.ClientSelectionne = client;
            // Assert
            Assert.Equal(client, viewModel.ClientSelectionne);
        }

        [Fact]
        public void ClientSelectionne_Setter_AssignsAndClearsProperties()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Client client = new Client(1, "Doe", "John", "email@email.com");

            // Act: assign a client
            viewModel.ClientSelectionne = client;

            // Assert: properties populated from client
            Assert.Equal(client, viewModel.ClientSelectionne);
            Assert.Equal(client.Nom, viewModel.Nom);
            Assert.Equal(client.Prenom, viewModel.Prenom);
            Assert.Equal(client.Courriel, viewModel.Courriel);

            // Act: clear selection
            viewModel.ClientSelectionne = null;

            // Assert: properties cleared
            Assert.Null(viewModel.ClientSelectionne);
            Assert.Equal(string.Empty, viewModel.Nom);
            Assert.Equal(string.Empty, viewModel.Prenom);
            Assert.Equal(string.Empty, viewModel.Courriel);
        }

        [Fact]
        public void Modifier_ShouldUpdateClient_WhenClientSelectionneIsNotNull()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Client client = new Client(1, "Doe", "John", "email@email.com");

            viewModel.ClientSelectionne = client;

            viewModel.Nom = "Smith";
            viewModel.Prenom = "Jane";
            viewModel.Courriel = "courriel@courriel.com";

            // Act
            viewModel.Modifier(null);
            // Assert

            _pDataServiceMockClient.Verify(ds => ds.Update(It.Is<Client>(c => c.Nom == "Smith" && c.Prenom == "Jane" && c.Courriel == "courriel@courriel.com")), Times.Once);

        }

        [Fact]
        public void Modifier_ShouldRestoreOriginalValues_WhenUpdateThrows()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Client client = new Client(1, "OrigNom", "OrigPrenom", "orig@mail.com");
            viewModel.ClientSelectionne = client;

            // New values in the view model's fields (user input)
            viewModel.Nom = "NewNom";
            viewModel.Prenom = "NewPrenom";
            viewModel.Courriel = "new@mail.com";

            // Configure Update to throw to exercise the catch block and restoration
            _pDataServiceMockClient.Setup(ds => ds.Update(It.IsAny<Client>())).Throws(new Exception("db error"));

            // Act
            viewModel.Modifier(null);

            // Assert: original client properties restored
            Assert.Equal("OrigNom", client.Nom);
            Assert.Equal("OrigPrenom", client.Prenom);
            Assert.Equal("orig@mail.com", client.Courriel);

            // And an error message was shown to the user
            _dialServiceMock.Verify(ds => ds.AfficherMessageErreur(It.Is<string>(s => s.Contains("db error"))), Times.Once);
        }

        [Fact]
        public void Retirer_ShouldCallUpdateCompte_WhenCompteSelectionneIsNotNull()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Compte compte = new Compte(1, "1234", 1000.0, 1);
            viewModel.CompteSelectionne = compte;
            viewModel.MontantTransaction = 200.0;
            // Act
            viewModel.Retirer(null);
            // Assert
            _pDataServiceMockCompte.Verify(ds => ds.Update(It.Is<Compte>(c => c.Balance == 800.0)), Times.Once);
        }

        [Fact]
        public void Deposer_ShouldCallUpdateCompte_WhenCompteSelectionneIsNotNull()
        {
            // Arrange
            BanqueViewModel viewModel = new BanqueViewModel(_dialServiceMock.Object, _pDataServiceMockClient.Object, _pDataServiceMockCompte.Object);
            Compte compte = new Compte(1, "1234", 1000.0, 1);
            viewModel.CompteSelectionne = compte;
            viewModel.MontantTransaction = 300.0;
            // Act
            viewModel.Deposer(null);
            // Assert
            _pDataServiceMockCompte.Verify(ds => ds.Update(It.Is<Compte>(c => c.Balance == 1300.0)), Times.Once);
        }
    }
}