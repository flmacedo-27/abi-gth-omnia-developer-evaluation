using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

/// <summary>
/// Command for creating a new customer.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a customer, 
/// including fullname, cpf_cnpj, email and phone number. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateCustomerResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateCustomerCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateCustomerCommand : IRequest<CreateCustomerResult>
{
    /// <summary>
    /// Sets the fullname of the customer to be created.
    /// </summary>
    public string Fullname { get; set; }

    /// <summary>
    /// Sets the cpf or cnpj of the customer to be created.
    /// </summary>
    public string CpfCnpj { get; set; }

    /// <summary>
    /// Sets the email address for the customer.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Sets the phone number for the customer.
    /// </summary>
    public string Phone { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
