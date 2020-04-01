namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeRepository : Repository<FakeEntity>, IFakeRepository
    {
        public FakeRepository(FakeContext context) : base(context)
        {
        }
    }
}
