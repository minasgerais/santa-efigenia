using System;
using System.Collections.Generic;

namespace Samwise.Abstractions.Models.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentar
    {
        public CamaraMunicipalCusteioParlamentar()
        {
            CamaraMunicipalCusteioParlamentarExpenseses = new List<CamaraMunicipalCusteioParlamentarExpenses>();
        }
        
        public string IdDocumentExtracted { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ExtractionDate { get; set; }
        
        public DateTimeOffset? Parsed { get; set; }
        
        public IEnumerable<CamaraMunicipalCusteioParlamentarExpenses> CamaraMunicipalCusteioParlamentarExpenseses { get; set; }

        public CamaraMunicipalCusteioParlamentar SetIdDocumentExtracted(string idDocument)
        {
            IdDocumentExtracted = idDocument;
            return this;
        }

        public CamaraMunicipalCusteioParlamentar SetExtrationDateWithDateNow()
        {
            ExtractionDate = DateTimeOffset.Now;
            return this;
        }
    }
}