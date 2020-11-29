using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Web_ECommerce.Controllers;

namespace Web_ECommerce.Models
{
    [LogActionFilter]
    public class HelpQrCode : BaseController
    {
        #region Construtores

        public HelpQrCode(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp InterfaceLogSistemaApp) : base(logger, userManager, InterfaceLogSistemaApp) { }

        #endregion

        #region Métodos

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);

                return stream.ToArray();
            }
        }

        private async Task<byte[]> GeraQrCode(string dadosBanco)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(dadosBanco, QRCodeGenerator.ECCLevel.H);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var bitmapBytes = BitmapToBytes(qrCodeImage);

            return bitmapBytes;
        }

        public async Task<IActionResult> Download(CompraUsuario compraUsuario, IWebHostEnvironment _environment)
        {
            using (var doc = new PdfDocument())
            {
                #region Configuração da folha

                var page = doc.AddPage();

                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;

                var graphics = XGraphics.FromPdfPage(page);
                var corFonte = XBrushes.Black;

                #endregion

                #region Numeração das Paginas

                int qtdPaginas = doc.PageCount;
                var numeracaoPagina = new XTextFormatter(graphics);

                numeracaoPagina.DrawString(Convert.ToString(qtdPaginas), new XFont("Arial", 10), corFonte, new XRect(575, 825, page.Width, page.Height));

                #endregion

                #region Logo

                var webRoot = _environment.WebRootPath;
                var logoFatura = string.Concat(webRoot, "/img/", "loja-virtual-1.png");

                XImage imagem = XImage.FromFile(logoFatura);

                graphics.DrawImage(imagem, 20, 5, 300, 50);

                #endregion

                #region Informação 1

                var relatorioCobranca = new XTextFormatter(graphics);
                var titulo = new XFont("Arial", 14, XFontStyle.Bold);

                relatorioCobranca.Alignment = XParagraphAlignment.Center;
                relatorioCobranca.DrawString("BOLETO ONLINE", titulo, corFonte, new XRect(0, 65, page.Width, page.Height));

                #endregion

                #region Informação 2

                var alturaTituloDetalhesY = 120;
                var detalhes = new XTextFormatter(graphics);
                var tituloInfo_1 = new XFont("Arial", 8, XFontStyle.Regular);

                detalhes.DrawString("Dados do banco", tituloInfo_1, corFonte, new XRect(25, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString("Banco Itau 004", tituloInfo_1, corFonte, new XRect(150, alturaTituloDetalhesY, page.Width, page.Height));

                alturaTituloDetalhesY += 9;

                detalhes.DrawString("Código Gerado", tituloInfo_1, corFonte, new XRect(25, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString("000000 000000 000000 000000", tituloInfo_1, corFonte, new XRect(150, alturaTituloDetalhesY, page.Width, page.Height));

                alturaTituloDetalhesY += 9;

                detalhes.DrawString("Quantidade:", tituloInfo_1, corFonte, new XRect(25, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString(compraUsuario.QuantidadeProdutos.ToString(), tituloInfo_1, corFonte, new XRect(150, alturaTituloDetalhesY, page.Width, page.Height));

                alturaTituloDetalhesY += 9;

                detalhes.DrawString("Valor Total:", tituloInfo_1, corFonte, new XRect(25, alturaTituloDetalhesY, page.Width, page.Height));
                detalhes.DrawString(compraUsuario.ValorTotal.ToString(), tituloInfo_1, corFonte, new XRect(150, alturaTituloDetalhesY, page.Width, page.Height));

                var tituloInfo_2 = new XFont("Arial", 8, XFontStyle.Bold);

                try
                {
                    var img = await GeraQrCode("Dados do banco aqui");
                    Stream streamImage = new MemoryStream(img);
                    XImage qrCode = XImage.FromStream(() => streamImage);

                    alturaTituloDetalhesY += 40;

                    graphics.DrawImage(qrCode, 140, alturaTituloDetalhesY, 310, 310);
                }
                catch(Exception erro)
                {
                    ViewBag.Erro = true;
                    ViewBag.Mensagem = erro.Message;
                }

                alturaTituloDetalhesY += 620;

                detalhes.DrawString("Canhoto com QrCode para pagamento online.", tituloInfo_2, corFonte, new XRect(20, alturaTituloDetalhesY, page.Width, page.Height));

                #endregion

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";

                    doc.Save(stream, false);

                    return File(stream.ToArray(), contentType, "BoletoLojaOnline.pdf");
                }
            }
        }

        #endregion
    }
}