
using GestionBanque.Models;
using GestionBanque.Models.DataService;
using System.Numerics;

namespace GestionBanque.Tests
{
    // Ce décorateur s'assure que toutes les classes de tests ayant le tag "Dataservice" soit
    // exécutées séquentiellement. Par défaut, xUnit exécute les différentes suites de tests
    // en parallèle. Toutefois, si nous voulons forcer l'exécution séquentielle entre certaines
    // suites, nous pouvons utiliser un décorateur avec un nom unique. Pour les tests sur les DataService,
    // il est important que cela soit séquentiel afin d'éviter qu'un test d'une classe supprime la 
    // bd de tests pendant qu'un test d'une autre classe utilise la bd. Bref, c'est pour éviter un
    // accès concurrent à la BD de tests!
    [Collection("Dataservice")]
    public class ClientSqliteDataServiceTest
    {
        private const string CheminBd = "..\\test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            Client? clientActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(clientAttendu, clientActuel);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void GetAll_ShouldBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            List<Client> clientsAttendus = new List<Client>
            {
                new Client(1, "Quentin", "Amar", "quentin@gmail.com"),
                new Client(2, "Tex", "Agère", "tex@gmail.com"),
                new Client(3, "Sarah", "Vigote", "sarah@gmail.com")
            };

            //INSERT INTO compte VALUES (NULL, '9864', 831.76, 1);
            //INSERT INTO compte VALUES(NULL, '2370', 493.04, 1);
            //INSERT INTO compte VALUES(NULL, '7640', 634.73, 2);
            //INSERT INTO compte VALUES(NULL, '7698', 906.72, 3);

            clientsAttendus[0].Comptes.Add(new Compte(1, "9864", 1.00, 1));
            clientsAttendus[0].Comptes.Add(new Compte(2, "2370", 493.04, 1));
            clientsAttendus[1].Comptes.Add(new Compte(3, "7640", 3.00, 2));
            clientsAttendus[2].Comptes.Add(new Compte(4, "7698", 913.62, 3));
            // Exécution
            IEnumerable<Client> clientsActuels = ds.GetAll();
            // Affirmation
            Assert.Equal(clientsAttendus, clientsActuels);

        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void RecupererCompte_ShouldBeValid()
        {
                       // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            List<Compte>comptesAttendus = new List<Compte>
            {
                new Compte(1, "9864", 831.76, 1),
                new Compte(2, "2370", 493.04, 1),
                new Compte(3, "7640", 634.73, 2),
                new Compte(4, "7698", 906.72, 3)
            };
            // Exécution
            IEnumerable<Client> clientsActuels = ds.GetAll();
            IEnumerable<Compte> comptesActuels = clientsActuels.SelectMany(c => c.Comptes);
            // Affirmation
            Assert.Equal(comptesAttendus, comptesActuels);


        }
    }
}
