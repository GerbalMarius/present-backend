namespace backend.Models.DTO;

public record DeskStatusData(long Id, string Name)
{
    public static DeskStatusData Empty => new(-1, string.Empty);
};