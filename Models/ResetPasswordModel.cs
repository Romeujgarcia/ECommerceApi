using System.ComponentModel.DataAnnotations;

public class ResetPasswordModel
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O token de redefinição de senha é obrigatório.")]
    public string? Token { get; set; }

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    public string? NewPassword { get; set; }
}
