using System.Threading.Tasks;
using UFID_Reader.Models.DbModels;

namespace UFID_Reader.Services;

public interface IKioskService
{
    Task RegisterKioskAsync(string serialNumber);
    Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber);
}

public class KioskService(IDbService dbService) : IKioskService
{
    public async Task RegisterKioskAsync(string serialNumber)
    {
        const string sql = "INSERT INTO kiosks (serial_num, room_num) VALUES (@SerialNumber, 'No Room')";
        await dbService.ExecuteAsync(sql, new { SerialNumber = serialNumber });
    }
    public async Task<Kiosk?> GetKioskBySerialNumberAsync(string serialNumber)
    {
        const string sql = "SELECT * FROM kiosks WHERE serial_num = @serialNumber";
        return await dbService.QuerySingleAsync<Kiosk>(sql, new { serialNumber });
    }
}