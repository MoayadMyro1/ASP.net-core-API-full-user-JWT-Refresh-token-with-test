using System;
using System.Collections.Generic;

namespace DriverApi.ModelsDtos;

public partial class PaymentTransactionDto
{
    public int Id { get; set; }

    public int PaymentId { get; set; }

    public string? GatewayResponse { get; set; }

    public int? TransactionStatus { get; set; }

    public string? ExternalTransactionId { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public virtual PaymentDto Payment { get; set; } = null!;
}
