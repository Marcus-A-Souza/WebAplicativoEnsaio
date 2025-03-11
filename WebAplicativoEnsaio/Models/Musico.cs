using System.ComponentModel.DataAnnotations;

namespace WebAplicativoEnsaio.Models
{
    public class Musico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Grupo { get; set; } // Ex: Grupo A ou Grupo B
        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        public string NumeroTelefone { get; set; } // Adicione esta propriedade whatsapp'
        public string UsuarioId { get; set; } // FK para usuário autenticado
        // Chave estrangeira para Ensaio
        public int EnsaioId { get; set; }
        public Ensaio Ensaio { get; set; }

        // Relacionamento muitos-para-muitos
       // public ICollection<Ensaio> Ensaios { get; set; } = new List<Ensaio>();
    }
}
