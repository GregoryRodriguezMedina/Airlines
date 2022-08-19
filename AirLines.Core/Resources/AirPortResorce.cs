
namespace AirLines.Core.Resources
{
    public class AirPortResorce
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AirPortResponse: AirPortResorce { }
    public class AirPortRequest: AirPortResorce { }
}
