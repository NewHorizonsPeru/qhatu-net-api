using System.Text.Json;

namespace Application.MainModule.DTO.Exceptions
{
    public class GenericException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        } 
    }
}
