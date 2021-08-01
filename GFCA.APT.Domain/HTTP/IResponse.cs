namespace GFCA.APT.Domain.HTTP
{
    public interface IResponse<TPayload>
    {
        TPayload data { get; set; }
    }
}
