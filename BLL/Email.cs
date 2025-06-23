using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace BLL
{
    public class Email
    {
        public void Enviar(Factura factura, Cliente cliente, Producto producto, List<DetalleFactura> listaDetalle)
        {
            byte[] pdfBytes = GenerarPDF(factura,cliente, producto, listaDetalle);

            MemoryStream pdfStream = new MemoryStream(pdfBytes);

            MailMessage email = new MailMessage();

            email.Subject = "Detalles de la Factura";

            email.To.Add(new MailAddress(cliente.Email));

            email.From = new MailAddress("beachhotelsa@gmail.com", "BigFoodServices");

            string body = "Estimado/a " + cliente.NombreCompleto + ",\n\nGracias por preferirnos ";
            email.Body = body;

            email.Attachments.Add(new Attachment(pdfStream, $"Factura_{cliente.cedulaLegal}.pdf"));

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;

            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential("beachhotelsa@gmail.com", "jkob lvln woiy ehqq");

            smtp.Send(email);

            email.Dispose();
            smtp.Dispose();
        }

        public byte[] GenerarPDF(Factura factura, Cliente cliente, Producto producto, List<DetalleFactura> listaDetalle)
        {
            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            document.Open();

            Paragraph paragraph = new Paragraph("BIG FOOD SERVICES S.A - Número de factura: " + factura.numero);
            document.Add(paragraph);

            document.Add(new Paragraph("DATOS DEL CLIENTE"));
            document.Add(new Paragraph("Nombre: " + cliente.NombreCompleto));
            document.Add(new Paragraph("Cédula: " + cliente.cedulaLegal));
            document.Add(new Paragraph("Estado: " + cliente.estado));
            document.Add(new Paragraph("Email: " + cliente.Email));
            document.Add(new Paragraph("Tipo de pago: " + factura.TipoPago));

            document.Add(new Paragraph("-----------------------------------------------"));
            decimal montoDescuento = Math.Round(factura.MontoDescuento, 2);
            decimal MontoImpuesto = Math.Round(factura.MontoImpuesto, 2);
            document.Add(new Paragraph("\nDatos de la compra:"));
            document.Add(new Paragraph("Tipo de documento: Factura de la compra"));
            document.Add(new Paragraph("Factura: " + factura.numero));
            document.Add(new Paragraph("Fecha de compra: " + factura.Fecha.ToString("dd/MM/yyyy")));
            document.Add(new Paragraph("-----------------------------------------------"));

            document.Add(new Paragraph("\nDetalle factura"));

            foreach (DetalleFactura detalle in listaDetalle)
            {
                document.Add(new Paragraph("Codigo Interno: " + detalle.codInterno));
                document.Add(new Paragraph("Descripcion: " + producto.Descripcion));
                document.Add(new Paragraph("Cantidad: " + detalle.cantidad));
                document.Add(new Paragraph("Precio unitario: ₡" + detalle.PrecioUnitario));
                document.Add(new Paragraph("Descuento: " + detalle.PorDescuento + "%"));
                document.Add(new Paragraph("Impuesto: " + detalle.PorImp + "%"));
                document.Add(new Paragraph("Subtotal: ₡" + detalle.Subtotal));
                document.Add(new Paragraph("-----------------------------------------------"));
            }

            document.Add(new Paragraph("-----------------------------------------------"));
            document.Add(new Paragraph("Monto descuento total: ₡" + montoDescuento));
            document.Add(new Paragraph("Monto Impuesto total: ₡" + MontoImpuesto));
            document.Add(new Paragraph("Total a pagar: ₡" + factura.Total));
            document.Add(new Paragraph("-----------------------------------------------"));
            document.Add(new Paragraph("Muchas gracias por su compra !!!! "));

            document.Close();

            byte[] pdfBytes = stream.ToArray();

            return pdfBytes;
        }
    }
}
