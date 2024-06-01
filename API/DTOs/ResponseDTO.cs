namespace API.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? data {  get; set; }
    }
}
