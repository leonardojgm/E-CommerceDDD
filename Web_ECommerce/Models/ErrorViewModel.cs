namespace Web_ECommerce.Models
{
    public class ErrorViewModel
    {
        #region Propriedades

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion
    }
}