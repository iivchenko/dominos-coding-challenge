namespace CodingChallenge.Application.Commands.CreateCupon;

public sealed class CreateCuponCommandHandler : IRequestHandler<CreateCuponCommand, CreateCuponCommandResponse>
{
    public async Task<CreateCuponCommandResponse> Handle(CreateCuponCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
