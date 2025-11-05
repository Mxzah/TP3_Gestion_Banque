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


    }
}
