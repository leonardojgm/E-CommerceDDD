using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Notifications
{
    public class Notifies
    {
        public Notifies()
        {
            Notifycoes = new List<Notifies>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notifycoes { get; set; }

        public bool ValidaPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(nomePropriedade))
            {
                Notifycoes.Add(new Notifies { Mensagem = "Campo Obrigatório", NomePropriedade = nomePropriedade });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrEmpty(nomePropriedade))
            {
                Notifycoes.Add(new Notifies { Mensagem = "Valor deve ser maior que 0", NomePropriedade = nomePropriedade });

                return false;
            }

            return true;
        }

        public bool ValidaPropriedadDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrEmpty(nomePropriedade))
            {
                Notifycoes.Add(new Notifies { Mensagem = "Valor deve ser maior que 0", NomePropriedade = nomePropriedade });

                return false;
            }

            return true;
        }
    }
}