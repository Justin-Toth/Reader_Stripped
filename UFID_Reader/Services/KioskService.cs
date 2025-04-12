using System.Threading.Tasks;
using UFID_Reader.Models.DbModels;

namespace UFID_Reader.Services;

public interface IKioskService
{
    Task<bool> IsKioskRegisteredAsync(string serialNumber);
    Task RegisterKioskAsync(string serialNumber);
    Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber);
}

public class KioskService(IDbService dbService) : IKioskService
{
    public async Task<bool> IsKioskRegisteredAsync(string serialNumber)
    {
        const string query = "SELECT COUNT(*) FROM kiosks WHERE serial_number = @SerialNumber";
        var count = await dbService.QuerySingleAsync<int>(query, new { SerialNumber = serialNumber });
        return count > 0;
    }

    public async Task RegisterKioskAsync(string serialNumber)
    {
        const string insert = "INSERT INTO kiosks (serial_number, registered_at) VALUES (@SerialNumber, NOW())";
        await dbService.ExecuteAsync(insert, new { SerialNumber = serialNumber });
    }
    public async Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber)
    {
        const string sql = "SELECT * FROM kiosks WHERE serial_num = @serialNumber";
        return await dbService.QuerySingleAsync<Kiosk>(sql, new { serialNumber });
    }
}