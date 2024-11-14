using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
            .Length(3, 100).WithMessage("O nome de usuário deve ter entre 3 e 100 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(user => user.PasswordHash)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
    }
}

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que 0.");
    }
}
