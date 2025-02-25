using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CpfCnpjValidator : AbstractValidator<string>
{
    public CpfCnpjValidator() 
    {
        RuleFor(customer => customer)
            .NotEmpty().WithMessage("The CPF/CNPJ is required.")
            .Must(BeValidCpfOrCnpj).WithMessage("The CPF/CNPJ is invalid.");
    }

    private bool BeValidCpfOrCnpj(string cpfCnpj)
    {
        if (string.IsNullOrWhiteSpace(cpfCnpj))
            return false;

        cpfCnpj = Regex.Replace(cpfCnpj, @"[^\d]", "");
        return cpfCnpj.Length == 11 || cpfCnpj.Length == 14;
    }
}
