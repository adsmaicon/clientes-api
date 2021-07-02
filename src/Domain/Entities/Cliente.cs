namespace Clientes.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string Nome { get; set; }

        public string Estado { get; set; }

        public string CPF { get; set; }
    }
}