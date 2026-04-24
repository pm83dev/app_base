using backend.repository.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.repository.DataManager;

public class DataDLL
{
    private readonly AppDbContext _context;
    public DataDLL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DataList> GetDataAsync()
    {
        var result = await _context.Data.ToListAsync();  // List<DataItem> dal DB
        return new DataList { Data = result };                   // wrappa nel contenitore
    }


}