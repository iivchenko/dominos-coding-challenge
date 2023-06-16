namespace CodingChallenge.Application.Commands.UpdateCupon;

public sealed class UpdateCuponCommandHandler : IRequestHandler<UpdateCuponCommand, UpdateCuponCommandResponse>
{
    public async Task<UpdateCuponCommandResponse> Handle(UpdateCuponCommand request, CancellationToken cancellationToken)
    {
        // iivc comment: 
        // Requirenments: Usages can only be incremented / decremented by 1 at a time.
        // The API can provide scalar value, domain can increment/decrement,
        //I can't change APIs - validation should be in the Application

        throw new NotImplementedException();
    }
}
