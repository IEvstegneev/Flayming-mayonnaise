using FuelAccounting.DataAccess;

namespace MaterialAccounting.API.Contracts
{
    public class RefuelDTO
    {
        private static string[] equipmentTableSimulation = new string[] { "Truck Volvo", "Truck Kamaz", "Excavator Hitachi", "Diesel generator Wilson" };

        public string Equipment { get; set; } 
        public int Value { get; set; }
        public DateTimeOffset Date { get; set; }
        public FuelType FuelType { get; set; }

        public int FromAccount { get; set; }
        public int FromTankId { get; set; }

        public int ToAccount { get; set; }
        public int ToTankId { get; set; }

        public RefuelDTO(Refuel refuel)
        {
            Equipment = equipmentTableSimulation[refuel.EquipmentId];
            Value = refuel.Value;
            Date = refuel.Date;
            FuelType = refuel.FuelType;
        }
    }
}