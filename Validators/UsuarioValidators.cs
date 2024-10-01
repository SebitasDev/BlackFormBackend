using BlackFormBackend.Models;
using BlackFormBackend.Models.DTOs.Request;
using FluentValidation;

namespace BlackFormBackend.Validators;

public class UsuarioValidators : AbstractValidator<UsuarioRequestDTO>
{
    public UsuarioValidators()
    {
        RuleFor(x => x.Nombre).NotEmpty().MinimumLength(5);
        RuleFor(x => x.NombreCompleto).NotEmpty().MinimumLength(7);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress();
        RuleFor(x => x.Contrasena).NotEmpty().MinimumLength(8);
    }
}