namespace Domain.DTOs;

public class GetPostIdDto
{
    private int? Id;

    public void SetId(int? id)
    {
        Id = id;
    }

    public int? ReturnId()
    {
        return Id;
    }
}