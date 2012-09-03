namespace PresentationGenerator.Core.Commands
{
    public class DeletePresentationCommand
    {
        public string Id { get; set; }

        public DeletePresentationCommand(string presentationid)
        {
            Id = presentationid;
        }
    }
}