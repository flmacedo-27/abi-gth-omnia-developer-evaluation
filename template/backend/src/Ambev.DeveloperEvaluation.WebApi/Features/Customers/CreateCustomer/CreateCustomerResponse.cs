﻿using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// API response model for CreateCustomer operation
/// </summary>
public class CreateCustomerResponse
{
    /// <summary>
    /// The unique identifier of the created customer
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The customer's full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The customer's cpf or cnpj
    /// </summary>
    public string CpfCnpj { get; set; } = string.Empty;

    /// <summary>
    /// The customer's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The customer's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the customer
    /// </summary>
    public CustomerStatus Status { get; set; }
}
