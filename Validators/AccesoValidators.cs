using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace BlackFormBackend.Validators;

public class AccesoValidators : AbstractValidator<LoginRequest>
{
    public AccesoValidators()
    {
        
    }
}