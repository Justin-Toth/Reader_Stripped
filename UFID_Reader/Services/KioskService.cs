using System.Threading.Tasks;
using UFID_Reader.Models;

namespace UFID_Reader.Services;

public interface IKioskService
{
    Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber);
}

public class KioskService(IDbService dbService) : IKioskService
{
    public async Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber)
    {
        const string sql = "SELECT * FROM kiosks WHERE serial_num = @serialNumber";
        return await dbService.QuerySingleAsync<Kiosk>(sql, new { serialNumber });
    }
}