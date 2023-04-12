namespace ViewModel
{
    public class ResultApi<T>
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public T? Result { get; set; }

        public IList<T>? Results { get; set; }
    }
}
