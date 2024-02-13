using Excercise_6.Data;

namespace Excercise_6.Services
{
    public interface ISixService
    {

    }
    public class SixService : ISixService
    {
        private readonly SixContext _context;
        public SixService(SixContext context)
        {
            _context = context;
        }
    }
}
