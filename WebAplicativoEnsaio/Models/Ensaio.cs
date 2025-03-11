namespace WebAplicativoEnsaio.Models
{
    public class Ensaio
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }


        // Relacionamento com músicas
        public ICollection<Musics> Musics { get; set; }
        public ICollection<Musico> Musicos { get; set; } = new List<Musico>();
        // Novo campo para armazenar o grupo selecionado
        public string GrupoSelecionado { get; set; } 
    }
}
