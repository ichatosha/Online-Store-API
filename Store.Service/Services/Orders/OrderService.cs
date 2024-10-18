﻿using Store.Core;
using Store.Core.Entities;
using Store.Core.Entities.Order;
using Store.Core.Repositories.Contract;
using Store.Core.Services.Contract;
using Store.Core.Services;
using Store.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Store.Core.Specifications;

namespace Store.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IBasketRepository _basketRepository;
        private readonly Core.Repositories.Contract.IGenericRepository<Product, int> _productRepo;

        public OrderService(IUnitOfWork unitOfWork ,IBasketService basketService ,IBasketRepository basketRepository , IGenericRepository<Product, int> productRepo)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _basketRepository = basketRepository;
            _productRepo = productRepo;
        }



        public async Task<OrderO> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            // order Item : 
            if(basket.Items.Count > 0)
            {
                foreach (var item in orderItems)
                {
                    // get data of the product from database is better : 
                    var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                    var productOrderItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productOrderItem, product.Price, item.Quantity);
                }
            }
    
            // delivetry method : 
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod,int>().GetByIdAsync(deliveryMethodId);

            // subtotal : 
            var subTotal = orderItems.Sum(I => I.Price *  I.Quantity);


            // TODO...
            var order = new OrderO(buyerEmail, shippingAddress,  deliveryMethod,orderItems , subTotal ,"" );

            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return order;

        }

        public async Task<OrderO?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId)
        {

            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<OrderO, int>().GetByIdWithSpecificationsAsync(spec);
            if(order is null) return null;
            return order;
        }

        public async Task<IEnumerable<OrderO>?> GetOrderForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<OrderO, int>().GetAllWithSpecificationsAsync(spec);
            if(orders is null) return null;
            return orders; 
        }
    }
}
