namespace WebApiApplication.Models
{
    public class StudentPatchResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
