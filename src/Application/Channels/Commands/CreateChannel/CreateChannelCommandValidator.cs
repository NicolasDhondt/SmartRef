using FluentValidation;

namespace SmartRef.Application.Channels.Commands.CreateChannel;

public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
{
    public CreateChannelCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }
}