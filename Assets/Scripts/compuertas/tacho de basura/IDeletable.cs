public interface IDeletable
{
    void Delete();               // Borrado definitivo o envío a pool
    string GetDisplayName();     // Para logs/feedback
}
