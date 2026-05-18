

namespace DriverApi.ModelsDtos;

public partial class PaymentDto
{

    public decimal Amount { get; set; }

    public string? Currency { get; set; }

    public int? PaymentMethod { get; set; }

    public int PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

}
