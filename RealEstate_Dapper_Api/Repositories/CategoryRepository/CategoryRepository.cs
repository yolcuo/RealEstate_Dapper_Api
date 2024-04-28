using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "Select * From Category";
            using(var connection=_context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();
            }
        }
        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            string query = "insert into Category (CategoryName, CategoryStatus) values (@categoryName, @categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", true);
            using(var connection=_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCategoryAsync(int id)
        {
            string query = "delete from Category where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using(var connection=_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            string query = "update Category set CategoryName=@categoryName, CategoryStatus=@categoryStatus where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", categoryDto.CategoryID);
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            using( var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<GetByIDCategoryDto> GetCategoryAsync(int id)
        {
            string query = "select * from Category where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query, parameters);
                return values;
            }
        }
    }
}
