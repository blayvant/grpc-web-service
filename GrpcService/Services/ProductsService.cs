using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcService
{
    public class ProductsService : Products.ProductsBase
    {
        public static List<Product> _allProducts = new();

        public override Task<Result> AddProduct(Product request, ServerCallContext context)
        {

            if (string.IsNullOrEmpty(request.Name))
                return Task.FromResult(new Result { Msg = "Product name can't be blank", Status = false });

            var product = _allProducts.FirstOrDefault(f => f.Code == request.Code);
            if (product != null)
            {
                product.Stock++;
                product.Name = request.Name;
            }
            else
            {
                request.Stock = 1;
                _allProducts.Add(request);
            }

            return Task.FromResult(new Result { Msg = "Product Added", Status = true });
        }


        public override async Task GetAllProducts(Empty request, IServerStreamWriter<Product> responseStream, ServerCallContext context)
        {
            foreach (var product in _allProducts)
            {
                await responseStream.WriteAsync(product);
            }
        }

    }
}
