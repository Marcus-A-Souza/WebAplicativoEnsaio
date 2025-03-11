namespace WebAplicativoEnsaio.Models
{
    public class Musics
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string YouTubeLink { get; set; }
        public DateTime CreatedAt { get; set; }

        // Referência ao Ensaio (opcional)
        public int? EnsaioId { get; set; }
        public Ensaio Ensaio { get; set; }
       
    }
}
