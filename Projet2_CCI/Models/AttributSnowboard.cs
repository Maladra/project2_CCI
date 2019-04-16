namespace Projet2_CCI.Donnee
{
    public class AttributSnowboard
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public AttributSnowboard(int id, string nom)
        {
            this.Id = id;
            this.Nom = nom;
        }
        public override string ToString()
        {
            return this.Nom;
        }
    }

}
