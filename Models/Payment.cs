using System;
using System.Collections.Generic;

namespace DriverApi.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int DriverSubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public string? Currency { get; set; }

    public int? PaymentMethod { get; set; }

    public int PaymentStatus { get; set; }

    public string? TransactionReference { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual DriverSubscription DriverSubscription { get; set; } = null!;

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
}
