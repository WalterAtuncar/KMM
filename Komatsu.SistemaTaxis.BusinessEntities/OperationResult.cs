namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public class OperationResult
    {
        public OperationResult()
        {
            Success = true;
            StatusCode = 200;
        }
        public int NewID { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Rows { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ObjectResult { get; set; }
        public string Type { get; set; }
        public int StatusCode { get; set; }
    }
}
