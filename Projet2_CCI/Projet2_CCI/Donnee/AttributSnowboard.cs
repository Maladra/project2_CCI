namespace Projet2_CCI.Donnee
{
    class AttributSnowboard
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public AttributSnowboard(string id, string nom)
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
