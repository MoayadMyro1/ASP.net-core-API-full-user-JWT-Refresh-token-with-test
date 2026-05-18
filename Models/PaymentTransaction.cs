using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class PaymentTransaction
{
    public int Id { get; set; }

    public int PaymentId { get; set; }

    public string? GatewayResponse { get; set; }

    public int? TransactionStatus { get; set; }

    public string? ExternalTransactionId { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public virtual Payment Payment { get; set; } = null!;
}
