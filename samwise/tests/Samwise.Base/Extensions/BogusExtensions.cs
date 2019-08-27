using System;
using Bogus;

namespace Samwise.Base.Extensions
{
    public static class BogusExtensions
    {
        private static Lazy<Faker> _fakerPtBr  => new Lazy<Faker>(new Faker("pt_BR"));
        public static Faker FakerPtBr = _fakerPtBr.Value;

    }
}