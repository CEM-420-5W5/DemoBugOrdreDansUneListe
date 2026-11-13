using DemoBugOrdeDeListe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test
{
    [TestClass]
    public sealed class TestOrdre
    {
        [TestMethod]
        public void TestOrdreSimple()
        {
            // Il faut faire un update-database pour que ce test puisse être exécuté!

            // ATTENTION: Dans un test unitaire, il est préférable d'utiliser une base de données en mémoire (InMemoryDatabase) pour éviter les effets de bord liés à l'état de la base de données.
            // Cependant, pour reproduire le problème spécifique lié à l'ordre des éléments dans une liste, nous utilisons ici SQL Server LocalDB.
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DemoBugOrdeDeListeContext;Trusted_Connection=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<DemoBugOrdeDeListeContext>()
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies(true)
                .Options;

            using var db = new DemoBugOrdeDeListeContext(options);
            
            Enfant enfant1 = new Enfant { Nom = "Enfant 1" };
            Enfant enfant2 = new Enfant { Nom = "Enfant 2" };
            db.Enfant.Add(enfant1);
            db.Enfant.Add(enfant2);
            // On sauvegarde les enfants pour figer leurs Ids.
            db.SaveChanges();
            Parent parent = new Parent { Enfants = new List<Enfant> { enfant2, enfant1 } };
            db.Parent.Add(parent);
            db.SaveChanges();

            int parentId = parent.Id;

            // On recharge l'entité depuis la base de données pour vérifier l'ordre des enfants.
            db.ChangeTracker.Clear();
            Parent parentFromDb = db.Parent.Single(p => p.Id == parentId);

            // La collection est chargée depuis la base, sans ordre garanti.
            Assert.AreEqual("Enfant 2", parentFromDb.Enfants[0].Nom);
            Assert.AreEqual("Enfant 1", parentFromDb.Enfants[1].Nom);
        }


        [TestMethod]
        public void TestOrdreAvecIndex()
        {
            // ATTENTION: Dans un test unitaire, il est préférable d'utiliser une base de données en mémoire (InMemoryDatabase) pour éviter les effets de bord liés à l'état de la base de données.
            // Cependant, pour reproduire le problème spécifique lié à l'ordre des éléments dans une liste, nous utilisons ici SQL Server LocalDB.
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DemoBugOrdeDeListeContext;Trusted_Connection=True;MultipleActiveResultSets=true";

            var options = new DbContextOptionsBuilder<DemoBugOrdeDeListeContext>()
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies(true)
                .Options;

            using var db = new DemoBugOrdeDeListeContext(options);
            using var transaction = db.Database.BeginTransaction();

            EnfantAvecIndex enfant1 = new EnfantAvecIndex { Nom = "Enfant 1" };
            EnfantAvecIndex enfant2 = new EnfantAvecIndex { Nom = "Enfant 2" };
            db.EnfantAvecIndex.Add(enfant1);
            db.EnfantAvecIndex.Add(enfant2);
            // On sauvegarde les enfants pour figer leurs Ids.
            db.SaveChanges();
            Parent parent = new Parent {  };
            parent.AjouterEnfantAvecIndex(enfant2);
            parent.AjouterEnfantAvecIndex(enfant1);
            db.Parent.Add(parent);
            db.SaveChanges();

            int parentId = parent.Id;

            // On recharge l'entité depuis la base de données pour vérifier l'ordre des enfants.
            db.ChangeTracker.Clear();
            Parent parentFromDb = db.Parent.Single(p => p.Id == parentId);
            var enfants = parentFromDb.ObtenirEnfantsTriesParIndex();

            // La collection est chargée depuis la base, sans ordre garanti.
            Assert.AreEqual("Enfant 2", enfants[0].Nom);
            Assert.AreEqual("Enfant 1", enfants[1].Nom);
        }
    }
}
