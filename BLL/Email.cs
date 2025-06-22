using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace BLL
{
    public class Email
    {
        public void Enviar(Factura factura, Cliente cliente, Producto producto, List<DetalleFactura> listaDetalle)
        {
            // Generar el archivo PDF
            byte[] pdfBytes = GenerarPDF(factura,cliente, producto, listaDetalle);

            // Crear el objeto MemoryStream para almacenar el archivo PDF
            MemoryStream pdfStream = new MemoryStream(pdfBytes);

            // Crear la instancia del objeto MailMessage
            MailMessage email = new MailMessage();

            // Asunto del correo
            email.Subject = "Detalles de la Factura";

            // Destinatarios del correo
            email.To.Add(new MailAddress(cliente.Email));

            // Remitente del correo
            email.From = new MailAddress("beachhotelsa@gmail.com", "BigFoodServices");

            // Cuerpo del correo
            string body = "Estimado/a " + cliente.NombreCompleto + ",\n\nGracias por preferirnos ";
            email.Body = body;

            // Adjuntar el archivo PDF al correo
            email.Attachments.Add(new Attachment(pdfStream, $"Factura_{cliente.cedulaLegal}.pdf"));

            // Configuración del protocolo de comunicación SMTP
            SmtpClient smtp = new SmtpClient();

            // Servidor de correo
            smtp.Host = "smtp.gmail.com";

            // Puerto de comunicación
            smtp.Port = 587;

            // Indicar si el buzón utiliza seguridad tipo SSL
            smtp.EnableSsl = true;

            // Indicar si el buzón utiliza credenciales por default
            smtp.UseDefaultCredentials = false;

            // Asignar las credenciales para el envío del correo
            smtp.Credentials = new NetworkCredential("beachhotelsa@gmail.com", "jkob lvln woiy ehqq");

            // Método para enviar el email
            smtp.Send(email);

            // Liberar recursos
            email.Dispose();
            smtp.Dispose();
        }

        public byte[] GenerarPDF(Factura factura, Cliente cliente, Producto producto, List<DetalleFactura> listaDetalle)
        {
            // Crear el documento
            Document document = new Document();

            // Crear el objeto MemoryStream para escribir el PDF
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);

            // Abrir el documento
            document.Open();

            // Agregar el contenido al documento
            Paragraph paragraph = new Paragraph("BIG FOOD SERVICES S.A - Número de factura: " + factura.numero);
            document.Add(paragraph);

            // Datos del cliente / huésped
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


            //List<DetalleFactura> detallesFactura = new List<DetalleFactura>();


            // Detalle de los productos adquiridos
            document.Add(new Paragraph("\nDetalle factura"));

            // Agregar los datos de los detalles de factura al documento
            foreach (DetalleFactura detalle in listaDetalle)
            {
                // Agregar el texto al documento
                document.Add(new Paragraph("Codigo Interno: " + detalle.codInterno));
                document.Add(new Paragraph("Descripcion: " + producto.Descripcion));
                document.Add(new Paragraph("Cantidad: " + detalle.cantidad));
                document.Add(new Paragraph("Precio unitario: ₡" + detalle.PrecioUnitario));
                document.Add(new Paragraph("Descuento: " + detalle.PorDescuento + "%"));
                document.Add(new Paragraph("Impuesto: " + detalle.PorImp + "%"));
                document.Add(new Paragraph("Subtotal: ₡" + detalle.Subtotal));
                // Agregar más datos de los detalles de factura según sea necesario
                document.Add(new Paragraph("-----------------------------------------------"));
            }

            document.Add(new Paragraph("-----------------------------------------------"));

            document.Add(new Paragraph("Monto descuento total: ₡" + montoDescuento));
            document.Add(new Paragraph("Monto Impuesto total: ₡" + MontoImpuesto));
            document.Add(new Paragraph("Total a pagar: ₡" + factura.Total));

            document.Add(new Paragraph("-----------------------------------------------"));

            document.Add(new Paragraph("Muchas gracias por su compra !!!! "));

            // Cerrar el documento
            document.Close();

            // Obtener los bytes del archivo PDF generado
            byte[] pdfBytes = stream.ToArray();

            return pdfBytes;
        }
    }
}
