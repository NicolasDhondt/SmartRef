using FluentValidation;

namespace SmartRef.Application.Channels.Commands.CreateProjectChannel;

public class CreateProjectChannelCommandValidator : AbstractValidator<CreateProjectChannelCommand>
{

    public CreateProjectChannelCommandValidator()
    {
        RuleFor(pc => pc.ProjectId).GreaterThan(0);
        RuleFor(pc => pc.ChannelId).GreaterThan(0);
    }

}