using Dapper.Contrib.Extensions;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using TicketPlatform.API.Model;
using TicketPlatform.API.Repositories.Interfaces;
using TicketPlatform.API.ServiceErrors;
using TicketPlatform.API.Services;

namespace TicketPlatform.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Configurations _configuration;

        public CartRepository(IOptions<Configurations> configurations)
        {
            _configuration = configurations.Value;
            _sqlConnection = new SqlConnection(_configuration.ConnectionString);
        }

        public List<Cart> GetAllCarts(QueryParameters parameters)
        {
            return _sqlConnection.GetAll<Cart>().ToList();
        }

        public ErrorOr<Cart> GetCartByUserId(int idUser)
        {
            var card = _sqlConnection.Get<Cart>(idUser);

            return card;
        }

        public int InsertCart(Cart cart)
        {
            return (int)_sqlConnection.Insert(cart);
        }

        public bool UpsertCart(int idUser, Cart cart)
        {
            cart.IdUser = idUser;

            return _sqlConnection.Update(cart);
        }

        public bool DeleteCart(int idUser)
        {
            return _sqlConnection.Delete(new Cart { IdUser = idUser });
        }

        public bool DeleteCarts(List<int> ids)
        {
            var cartsToDelete = ids.Select(id => new Cart { IdUser = id }).ToList();

            return _sqlConnection.Delete(cartsToDelete);
        }

        public void SendEmail(string toEmail)
        {
            string fromEmail = "confirmation.ticketis@gmail.com";
            string password = "gyig oodb ssrm iiah";

            string subject = "Confirmare comanda bilete online";
            string body = "Stimate utilizator, \r\n <br> Acest mail contine codul QR al biletelor achizitionate pe baza carora se face accesul la eveniment. \r\n <br> Multumim pentru comanda! \r\n <br>";

            try
            {
                using (MailMessage mail = new())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using SmtpClient smtp = new("smtp.gmail.com");
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(fromEmail, password);

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
