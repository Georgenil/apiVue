using apiNux.Data;
using apiNux.Domain;
using apiNux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace apiNux.Services
{
    public class SupplierService
    {
        private readonly ApplicationDbContext db;
        public SupplierService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<ResponseModel> ListSuppliers()
        {
            return new ResponseModel(200, await db.Suppliers.Include(x => x.Material).AsNoTracking().ToListAsync());
        }
        public async Task<ResponseModel> AddSupplier(Supplier supplier)
        {
            try
            {
                Supplier findSupplier = await db.Suppliers.FirstOrDefaultAsync(x => x.Name.ToLower().Trim()
                .Equals(supplier.Name.ToLower().Trim()));

                if (findSupplier == null)
                {
                    db.Suppliers.Add(supplier);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Fornecedor adicionado");
                }
                else
                {
                    return new ResponseModel(500, "Já existe um fornecedor com esse nome");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new ResponseModel(500, "Não foi possível adicionar o fornecedor");
        }
    }
}
