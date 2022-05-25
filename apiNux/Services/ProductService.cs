using apiNux.Data;
using apiNux.Domain;
using apiNux.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace apiNux.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext db;
        private const string PRODUCT_PATH = "wwwroot/Uploads/Product";
        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ResponseModel> ListProducts()
        {
            try
            {
                var findProducts = await db.Products.Include(x => x.UploadDocument).Include(x => x.Supplier).AsNoTracking().ToListAsync();
                if (findProducts != null)
                {
                    return new ResponseModel(200, findProducts);
                }
                else { return new ResponseModel(404, "Produtos não encontrados"); }
            }
            catch
            {
                return new ResponseModel(500, "Erro ao listar produtos");
            }
        }
        public async Task<ResponseModel> GetProductById(int productId)
        {
            try
            {
                var findProduct = db.Products.Include(x => x.UploadDocument).Include(x => x.Supplier)
                    .AsNoTracking().FirstOrDefault(x => x.Id == productId);
                if (findProduct == null)
                {
                    return new ResponseModel(404, "Produto não foi encontrado");
                }
                return new ResponseModel(200, findProduct);

            }
            catch
            {
                return new ResponseModel(500, "Erro ao encontrar produto por id");
            }

        }
        public ProductViewModel ListWithFilter(ProductFilterModel filter)
        {
            try
            {
                IQueryable<Product> products = db.Products.Include(x => x.UploadDocument).Include(x => x.Supplier).OrderBy(x => x.CreateDate);

                products = filter.Search != "" && filter.Search != null ? db.Products.Where(x => x.Name.Contains(filter.Search)) : products;
                products = filter.InitialDate != null ? db.Products.Where(x => x.UpdateDate >= filter.InitialDate) : products;
                products = filter.EndDate != null ? db.Products.Where(x => x.UpdateDate <= filter.EndDate) : products;

                ProductViewModel model = new ProductViewModel(products, new Utils.Pager(products.Count(), filter.Page));
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ResponseModel GetDocument(int uploadDocumentId)
        {
            try
            {
                Document findDocument = db.Documents.AsNoTracking().FirstOrDefault(x => x.Id.Equals(uploadDocumentId));

                if (findDocument != null)
                {
                    string filePath = findDocument.FilePath;

                    byte[] fileArray = File.ReadAllBytes(findDocument.FilePath);
                    string covertedFile = Convert.ToBase64String(fileArray);

                    return new ResponseModel(200, covertedFile);
                }
                else
                {
                    return new ResponseModel(404, "Este produto não possui arquivo");
                }
            }
            catch
            {
                return new ResponseModel(500, "Erro ao buscar o documento");
            }
        }
        public async Task<ResponseModel> AddProduct(Product product)
        {
            try
            {
                if (product != null)
                {

                    product.CreateDate = DateTime.Now;
                    product.UpdateDate = DateTime.Now;

                    EntityEntry<Product> result = db.Add(product);
                    await db.SaveChangesAsync();

                    if (product.UploadDocument != null)
                    {

                        string fileName = string.Empty;
                        string filePath = string.Empty;
                        string fileBase64 = product.UploadDocument.File.Split(",")[1];

                        byte[] convertedFile = Convert.FromBase64String(fileBase64);

                        filePath = $"{PRODUCT_PATH}/{result.Entity.Id}";

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        else
                        {
                            DirectoryInfo info = new DirectoryInfo(filePath);
                            FileInfo[] files = info.GetFiles();

                            if (files.Length > 0)
                            {
                                files[^1].Delete();
                            }
                        }

                        fileName = $"{filePath}_CoberturaProduto{product.UploadDocument.FileType}";


                        product.UploadDocument.FilePath = fileName;

                        using FileStream stream = new FileStream(fileName, FileMode.Create);
                        Stream mStream = new MemoryStream(convertedFile);
                        mStream.CopyTo(stream);
                        stream.Close();

                        product.UploadDocument.UploadDate = DateTime.Now;

                        db.Update(product.UploadDocument);
                        await db.SaveChangesAsync();
                    }

                    return new ResponseModel(200, "Produto Criado");
                }
                else
                {
                    return new ResponseModel(404, "Produto Vazio");
                }

            }
            catch (Exception e)
            {
                db.Remove(db.Products.Where(x => x.Name == product.Name).FirstOrDefault());
                await db.SaveChangesAsync();
                throw e;

            }
        }

        public async Task<ResponseModel> AddDocument(Product product)
        {
            try
            {
                Product findProduct = await db.Products.Include(x => x.UploadDocument).FirstOrDefaultAsync(x => x.Id.Equals(product.Id));

                if (findProduct != null)
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;
                    string fileBase64 = product.UploadDocument.File.Split(",")[1];

                    byte[] convertedFile = Convert.FromBase64String(fileBase64);

                    filePath = $"{PRODUCT_PATH}/{product.Id}";

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    else
                    {
                        DirectoryInfo info = new DirectoryInfo(filePath);
                        FileInfo[] files = info.GetFiles();

                        if (files.Length > 0)
                        {
                            files[^1].Delete();
                        }
                    }

                    fileName = $"{filePath}_CoberturaProduto{product.UploadDocument.FileType}";


                    product.UploadDocument.FilePath = fileName;

                    using FileStream stream = new FileStream(fileName, FileMode.Create);
                    Stream mStream = new MemoryStream(convertedFile);
                    mStream.CopyTo(stream);
                    stream.Close();

                    product.UploadDocument.UploadDate = DateTime.Now;

                    findProduct.UploadDocument = product.UploadDocument;

                    db.Update(findProduct.UploadDocument);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Arquivo adicionaddo com sucesso");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new ResponseModel(500, "Erro ao inserir produto");
        }

        public async Task<ResponseModel> UpdateProduct(Product product)
        {
            try
            {
                Product findProduct = db.Products.FirstOrDefault(x => x.Id == product.Id);
                if (findProduct != null)
                {
                    findProduct.Name = product.Name;
                    findProduct.Payment = product.Payment;
                    findProduct.Price = product.Price;
                    findProduct.QuoteObservation = product.QuoteObservation;
                    findProduct.Status = product.Status;
                    findProduct.Tariff = product.Tariff;
                    findProduct.BuyObservation = product.BuyObservation;
                    findProduct.DeliveryTime = product.DeliveryTime;
                    findProduct.Discont = product.Discont;
                    findProduct.Installments = product.Installments;
                    findProduct.SupplierId = product.SupplierId;
                    findProduct.UpdateDate = DateTime.Now;
                    db.Products.Update(findProduct);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Produto atualizado");
                }
                return new ResponseModel(404, "Não foi possível encontrar o produto");

            }
            catch
            {
                return new ResponseModel(500, "Erro ao atualizar produto");
            }
        }

        public async Task<ResponseModel> ChangeStatusProduct(int productId)
        {
            try
            {
                Product product = await db.Products.FirstOrDefaultAsync(x => x.Id.Equals(productId));

                if (product != null)
                {
                    product.Status = !product.Status;

                    db.Update(product);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, product);
                }
                return new ResponseModel(404, "Produto não encontrado");

            }
            catch
            {
                return new ResponseModel("Erro ao alterar status do produto", 500);
            }
        }

    }
}
