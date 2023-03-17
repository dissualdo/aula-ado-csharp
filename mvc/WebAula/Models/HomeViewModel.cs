using WebAula.Domain;

namespace WebAula.Models
{ 
    public class HomeViewModel
    {
        /// <summary>
        /// Lista de clientes
        /// </summary>
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
