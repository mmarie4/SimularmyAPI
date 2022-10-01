namespace SimularmyAPI.Middleware
{
    public class ErrorResponse
    {
        public string Error { get; }

        public ErrorResponse(string msg)
        {
            Error = msg;
        }
    }
}
