using Entities.Entities.Enums;
using Entities.Notifications;
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
        public EstadoCompra Estado { get; set; }

        [Column("CUS_QTD")] 
        [Display(Name = "Quantidade")]
        public int QtdCompra { get; set; }

        [Column(Order = 1)] 
        [Display(Name = "Usuário")] 
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        #endregion

        #region Relacionamentos

        public virtual Produto Produto { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion
    }
}