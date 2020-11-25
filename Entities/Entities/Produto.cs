using Entities.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("TB_PRODUTO")]
    public class Produto : Notifies
    {
        #region Propriedades

        [Column("PRD_ID")] 
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("PRD_NOME")] 
        [Display(Name = "Nome")] 
        [MaxLength(255)] 
        public string Nome { get; set; }

        [Column("PRD_DESCRICAO")] 
        [Display(Name = "Descrição")] 
        [MaxLength(150)]
        public string Descricao { get; set; }

        [Column("PRD_OBSERVACAO")] 
        [Display(Name = "Observação")] 
        [MaxLength(20000)]
        public string Observacao { get; set; }

        [Column("PRD_VALO")] 
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("PRD_QTD_ESTOQUE")] 
        [Display(Name = "Quantidade Estoque")]
        public int QtdEstoque { get; set; }

        [Column(Order = 1)] 
        [Display(Name = "Usuário")] 
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Column("PRD_ESTADO")] 
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Column("PRD_DATA_CADASTRO")] 
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("PRD_DATA_ALTERACAO")] 
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }

        [NotMapped]
        public int IdProdutoCarrinho { get; set; }

        [NotMapped]
        public int QtdCompra { get; set; }

        #endregion

        #region Relacionamentos

        public virtual ApplicationUser ApplicationUser { get; set; }

        #endregion
    }
}