namespace dotnet_rpg.Dtos.Character
{
    public class UpdateCharacterDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? HitPoints { get; set; }
        public int? Strength { get; set; }
        public int? Defence { get; set; }
        public int? Intelligence { get; set; }
        public RpgClass? Class { get; set; }
    }
}