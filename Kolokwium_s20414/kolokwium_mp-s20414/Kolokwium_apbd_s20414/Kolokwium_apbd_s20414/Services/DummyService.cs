using Kolokwium_apbd_s20414.Data;

namespace Kolokwium_apbd_s20414.Services
{
    public interface IDummyService
    {

    }
    public class DummyService : IDummyService
    {
        private readonly KolokwiumContext _context;
        public DummyService(KolokwiumContext context)
        {
            _context = context;
        }
    }
}
