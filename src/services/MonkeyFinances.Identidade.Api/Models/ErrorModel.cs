namespace MonkeyFinances.Identidade.Api.Models
{
    public class ErrorModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensagem { get; set; } = null!;
    }

    public class ResponseResult
    {
        public string Title { get; set; } = null!;
        public int Status { get; set; }
        public ResponseErrorMessages? Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; } = null!;
    }
}
