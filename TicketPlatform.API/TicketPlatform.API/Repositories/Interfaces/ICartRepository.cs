﻿using ErrorOr;
using TicketPlatform.API.Model;

namespace TicketPlatform.API.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public List<Cart> GetAllCarts(QueryParameters parameters);
        public ErrorOr<Cart> GetCartByUserId(int idUser);
        public int InsertCart(Cart cart);
        public int UpsertCart(Cart cart);
        public bool DeleteCart(int idUser, int idEvent);
        public bool DeleteCarts(List<int> ids);
        public void SendEmail(string toEmail);
    }
}