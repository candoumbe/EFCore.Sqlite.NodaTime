using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using VerifyTests.EntityFramework;
using VerifyXunit;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Sqlite
{
    public class InstantQueryTests : QueryTests<Instant>
    {
        public static readonly Instant Value = LocalDateTimeQueryTests.Value.InUtc().ToInstant();

        public InstantQueryTests() : base(x => x.Instant)
        {
        }

        [Fact]
        public void Roundtrip()
        {
            Assert.Equal(Value, Query.Single());
        }

        [Fact]
        public Task GetCurrentInstant_From_Instance()
        {
            SqlRecording.StartRecording();
            _ = Query.Single(x => x < SystemClock.Instance.GetCurrentInstant());
            return Verifier.Verify(SqlRecording.FinishRecording());
        }
    }
}