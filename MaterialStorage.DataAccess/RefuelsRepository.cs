namespace FuelAccounting.DataAccess
{
    public class RefuelsRepository : IRepository<Refuel>
    {
        private static string _directoryName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openLoops");

        static RefuelsRepository()
        {
            Directory.CreateDirectory(_directoryName);
        }

        public async Task<IEnumerable<Refuel>> GetAllAsync()
        {
            var files = Directory.GetFiles(_directoryName);
            var refuels = new List<Refuel>();
            foreach (var file in files)
            {
                refuels.Add(await JsonHelper.ReadAsync<Refuel>(file));
            }
            return refuels;
        }

        public async Task<Refuel> FindAsync(Guid id)
        {
            if (RefuelExists(id, out string filePath))
            {
                return await JsonHelper.ReadAsync<Refuel>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<Refuel> AddAsync(Refuel refuel)
        {
            if (RefuelExists(refuel.Id, out string filePath))
            {
                throw new InvalidOperationException($"File in path {filePath} cannot be created.");
            }
            else
            {
                await JsonHelper.WriteAsync(refuel, filePath);
                var result = await JsonHelper.ReadAsync<Refuel>(filePath);
                return result;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            if (RefuelExists(id, out string filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
            else
            {
                throw new InvalidOperationException($"File in path {filePath} cannot be deleted.");
            }
        }

        public async Task UpdateAsync(Guid id, Refuel newRefuel)
        {
            if (RefuelExists(id, out string filePath))
            {
                var refuel = await JsonHelper.ReadAsync<Refuel>(filePath);

                await JsonHelper.WriteAsync(newRefuel, filePath);
            }
            else
            {
                throw new InvalidOperationException($"File in path {filePath} cannot be updated.");
            }
        }

        private static bool RefuelExists(Guid id, out string filePath)
        {
            filePath = Path.Combine(_directoryName, $"{id}.json");
            return File.Exists(filePath);
        }
    }
}