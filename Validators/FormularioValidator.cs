using BlackFormBackend.Models;
using BlackFormBackend.Models.DTOs.Request;
using FluentValidation;

namespace BlackFormBackend.Validators;

public class FormularioValidator : AbstractValidator<FormularioRequestDTO>
{
    public FormularioValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.Tematica).IsInEnum();
        RuleFor(x => x.esPublico).NotEmpty();
        RuleFor(x => x.esActivo).NotEmpty();
        RuleFor(x => x.UsuarioId).NotEmpty();
        RuleFor(x => x.Categoria)
            .NotEmpty()
            .Must(c => c.Count > 0);
    }
}
