namespace DemoBugOrdeDeListe.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public virtual List<Enfant> Enfants { get; set; }
        public virtual List<EnfantAvecIndex> EnfantsAvecIndex { get; set; }

        public void AjouterEnfantAvecIndex(EnfantAvecIndex enfant)
        {
            if (EnfantsAvecIndex == null)
            {
                EnfantsAvecIndex = new List<EnfantAvecIndex>();
            }
            enfant.Index = EnfantsAvecIndex.Count; // Assigner l'index basé sur le nombre actuel d'enfants
            EnfantsAvecIndex.Add(enfant);
        }

        public void RetirerEnfantAvecIndex(EnfantAvecIndex enfant)
        {
            EnfantsAvecIndex.Remove(enfant);
            // Réindexer les enfants restants
            for (int i = 0; i < EnfantsAvecIndex.Count; i++)
            {
                EnfantsAvecIndex[i].Index = i;
            }
        }

        public List<EnfantAvecIndex> ObtenirEnfantsTriesParIndex()
        {
            return EnfantsAvecIndex.OrderBy(e => e.Index).ToList();
        }
    }
}
