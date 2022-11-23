namespace MaterialAccounting.API.Contracts
{
    public class CreateRefuelRequest
    {
        public int EquipmentId { get; set; } 
        public int TankId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Value { get; set; }
    }
}