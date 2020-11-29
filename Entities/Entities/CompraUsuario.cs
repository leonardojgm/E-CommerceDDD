using Entities.Entities.Enums;
using Entities.Notifications;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_COMPRA_USUARIO")]
    public class CompraUsuario : Notifies
    {
        #region Propriedades

        [Column("CUS_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column(Order = 1)]
        [Display(Name = "Produto")]
        [ForeignKey("TB_PRODUTO")]
        public int IdProduto { get; set; }

        [Column("CUS_ESTADO")]
        [Display(Name = "Estado")]
        public EnumEstadoCompra Estado { get; set; }

        [Column("CUS_QTD")]
        [Display(Name = "Quantidade")]
        public int QtdCompra { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        [Display(Name = "Usuário")]
        public string UserId { get; set; }

        [NotMapped]
        [Display(Name = "Quantidade Total")]
        public int QuantidadeProdutos { get; set; }

        [NotMapped]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        [Display(Name = "Endereço de entrega")]
        public string EnderecoCompleto { get; set; }

        [ForeignKey("TB_COMPRA")]
        [Column(Order = 1)]
        [Display(Name = "Compra")]
        public int IdCompra { get; set; }

        #endregion

        #region Relacionamentos

        public virtual Produto Produto { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Compra Compra {get; set;}

        [NotMapped]
        public List<Produto> ListaProdutos { get; set; }

        #endregion
    }
}