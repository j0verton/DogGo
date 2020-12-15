namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        System.Collections.Generic.List<Walk> GetWalksByWalkerId(int id);
    }
}