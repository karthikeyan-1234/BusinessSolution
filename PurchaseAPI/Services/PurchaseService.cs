﻿using CommonLibrary.Models.DTOs;

using MediatR;

using PurchaseAPI.CQRS.Commands;
using PurchaseAPI.CQRS.Queries;

namespace PurchaseAPI.Services
{
    public class PurchaseService : IPurchaseService
    {
        IMediator mediator;

        public PurchaseService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<PurchaseDTO>? AddNewPurchaseAsync(PurchaseDTO newPurchase)
        {
            var cmd = new AddPurchaseCommand(newPurchase);
            var result = await mediator.Send(cmd);
            return result;
        }

        public async Task<IEnumerable<PurchaseDTO>> GetAllPurchases()
        {
            var qry = new GetAllPurchasesQuery();
            var result = await mediator.Send(qry);
            return result;
        }
    }
}
