using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;

/// <summary>
/// Command for creating a new branch.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a branch, 
/// including fullname, cpf_cnpj, email and phone number. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateBranchResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateBranchValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateBranchCommand : IRequest<CreateBranchResult>
{
    /// <summary>
    /// Sets the name of the branch to be created.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Sets the code of the branch to be created.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Sets the city of the branch to be created.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Sets the state of the branch to be created.
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// Sets the country of the branch to be created.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Sets the postal code of the branch to be created.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Sets the phone number for the branch.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Sets the email address for the branch.
    /// </summary>
    public string Email { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateBranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
