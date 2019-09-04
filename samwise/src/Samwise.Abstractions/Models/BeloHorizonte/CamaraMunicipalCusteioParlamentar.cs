using System;
using System.Collections.Generic;

namespace Samwise.Abstractions.Models.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentar
    {
        public CamaraMunicipalCusteioParlamentar()
        {
            Expenses = new List<CamaraMunicipalCusteioParlamentarExpenses>();
        }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ExtractionDate { get; set; }
        
        public DateTimeOffset? Parsed { get; set; }
        
        public IEnumerable<CamaraMunicipalCusteioParlamentarExpenses> Expenses { get; set; }

        public CamaraMunicipalCusteioParlamentar SetIdDocumentExtracted(string idDocument)
        {
            Id = idDocument;
            return this;
        }

        public CamaraMunicipalCusteioParlamentar SetExtrationDateWithDateNow()
        {
            ExtractionDate = DateTimeOffset.Now;
            return this;
        }
    }
}