using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Samwise.Abstractions.Models.BeloHorizonte
{
    public class CamaraMunicipalCusteioParlamentar
    {
        public CamaraMunicipalCusteioParlamentar()
        {
            Expenses = new List<CamaraMunicipalCusteioParlamentarExpenses>();
        }
        
        public string IdParliamentary { get; set; }
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

        public CamaraMunicipalCusteioParlamentar SetDateExtractedWithDateNow()
        {
            ExtractionDate = DateTimeOffset.Now;
            return this;
        }

        public CamaraMunicipalCusteioParlamentar SetIdParliamentaryExtracted(string filterParliamentary)
        {
            IdParliamentary = Regex.Match(filterParliamentary, @"codVereador=(?<cdVereador>\d\w+)").Groups["cdVereador"].Value;
            return this;
        }
    }
}