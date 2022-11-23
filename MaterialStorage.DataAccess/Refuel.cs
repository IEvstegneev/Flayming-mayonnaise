using Microsoft.VisualBasic.FileIO;

namespace FuelAccounting.DataAccess
{
    public record Refuel  //: MaterialTransfer, ITransfer
    {
        //FuelTransfer
        public Guid Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Date { get; set; }
        public FuelType FuelType { get; set; }

        public int FromAccount { get; set; }
        public int FromTankId { get; set; }

        public int ToAccount { get; set; }
        public int ToTankId { get; set; }

        //Refuel
        public int EquipmentId { get; set; }

    }
}