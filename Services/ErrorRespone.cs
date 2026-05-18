namespace DriverApi.Services
{
    public class ErrorRespone<T>  
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = null!;
        public List<string> Errors { get; set; } = null!;

    }
}
