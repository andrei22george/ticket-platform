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
using Dapper;

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
            return _sqlConnection.Query<Cart>("SELECT * FROM Cart").ToList();
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

        public bool UpsertCart(int idUser, int idEvent, Cart cart)
        {
            cart.IdUser = idUser;
            cart.IdEvent = idEvent;

            return _sqlConnection.Update(cart);
        }

        public bool DeleteCart(int idUser, int idEvent)
        {
            return _sqlConnection.Delete(new Cart { IdUser = idUser, IdEvent = idEvent });
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
            string body = "Stimate utilizator, <br><br> Acest mail contine codul QR al biletelor achizitionate pe baza carora se face accesul la eveniment. <br><br> Multumim pentru comanda! <br><br>";

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
