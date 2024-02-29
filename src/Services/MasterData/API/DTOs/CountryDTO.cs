using Core;

namespace MasterData.DTOs
{
    public class CountryDTO
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public EnumStatus Status { get; set; }
    }


    public class CountryResponseDTO: CountryDTO
    {
        public int Id { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
