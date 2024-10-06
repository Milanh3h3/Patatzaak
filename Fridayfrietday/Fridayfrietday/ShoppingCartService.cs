using Fridayfrietday.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ShoppingCartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "ShoppingCart";

    public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<OrderDetail> GetCartItems()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cartJson = session.GetString(CartSessionKey);
        return cartJson != null ? JsonConvert.DeserializeObject<List<OrderDetail>>(cartJson) : new List<OrderDetail>();
    }

    public void AddToCart(OrderDetail orderDetail)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cartItems = GetCartItems();

        var existingItem = cartItems.Find(item => item.ProductId == orderDetail.ProductId && item.SelectedSauces == orderDetail.SelectedSauces);
        if (existingItem != null)
        {
            existingItem.Quantity += orderDetail.Quantity;
        }
        else
        {
            cartItems.Add(orderDetail);
        }

        session.SetString(CartSessionKey, JsonConvert.SerializeObject(cartItems));
    }

    public void ClearCart()
    {
        _httpContextAccessor.HttpContext.Session.Remove(CartSessionKey);
    }
}