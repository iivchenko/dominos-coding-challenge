namespace CodingChallenge.Application.Commands.UpdateCupon;

public sealed class UpdateCuponCommand : IRequest<UpdateCuponCommandResponse>
{
    public string Name { get; set; }
}
