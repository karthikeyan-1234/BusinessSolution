using CommonLibrary;
using CommonLibrary.Models;

using Microsoft.EntityFrameworkCore;


namespace PurchaseAPI.Contexts
{
    public interface IPurchaseDBContext: IDbContext
    {
        DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        DbSet<Purchase> Purchases { get; set; }
    }
}