using MasterData.Domain;

namespace MasterData.Application.Models
{
    public class StateModel
    {
        public required int CountryId { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public EnumStatus Status { get; set; }
    }


    public class StateResponseModel : CountryModel
    {
        public int Id { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
