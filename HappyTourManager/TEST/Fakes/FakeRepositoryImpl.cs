using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.Fakes
{
    internal class FakeRepositoryImpl : FakeRepository<Tourguide>
    {
        public FakeRepositoryImpl(IEnumerable<Tourguide> expected) : base(expected)
        {

        }
    }
}
