﻿using EshopAspCore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Sales
{
    public interface IOrderService
    {
        public Task<bool> CheckOutOrders(CheckOutRequest request);
        public Task<List<OrderViewModel>> GetAll(OrderGetRequest request);
        public Task<OrderViewModel> GetById(int id, string languageId);
    }
}
