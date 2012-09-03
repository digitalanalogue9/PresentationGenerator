namespace PresentationGenerator.Core.Documents
{
    public interface IDocument
    {
        //Raven Id ==
        string Id { get; set; }

    }

    public interface ISapObject
    {
        //SAP Id
        string Number { get; set; }
        //SAP Name
        string Name { get; set; }
    }



}