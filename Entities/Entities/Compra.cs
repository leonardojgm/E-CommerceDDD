using Entities.Entities.Enums;
using Entities.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_COMPRA")]
    public class Compra : Notifies
    {
        #region Propriedades

        [Column("COM_ID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("COM_ESTADO")]
        [Display(Name = "Estado")]
        public EnumEstadoCompra Estado { get; set; }

        [Column("COM_DATA_COMPRA")]
        [Display(Name = "Data Compra")]
        public DateTime? DataCompra { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        [Display(Name = "Usuário")]
        public string UserId { get; set; }

        #endregion

        #region Relacionamentos

        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion
    }
}