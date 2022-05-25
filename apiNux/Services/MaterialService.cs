using apiNux.Data;
using apiNux.Domain;
using apiNux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace apiNux.Services
{
    public class MaterialService
    {
        private readonly ApplicationDbContext db;

        public MaterialService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<ResponseModel> List()
        {
            var findMaterials = db.Materials.AsNoTracking();
            return new ResponseModel(200, findMaterials);
        }
        public async Task<ResponseModel> AddMaterial(Material material)
        {
            try
            {
                if (material != null)
                {
                    db.Materials.Add(material);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Material adicionado com sucesso!");
                }
                else
                {
                    return new ResponseModel(404, "Não é possível adicionar um material vazio.");
                }

            }
            catch
            {
                return new ResponseModel(500, "Erro ao adicionar material");
            }
        }
    }
}
