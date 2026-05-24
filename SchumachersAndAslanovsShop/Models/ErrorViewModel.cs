namespace SchumachersAndAslanovsShop.Models
//This class represents the model used to capture error information for display in error views. It includes a property for the request ID, which can be used for tracking and debugging purposes, and a computed property to determine whether the request ID should be shown based on its presence. This model is typically used in conjunction with ASP.NET Core's error handling mechanisms to provide users with feedback when an error occurs.
{

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
