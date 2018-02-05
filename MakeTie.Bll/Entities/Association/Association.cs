using System.Collections.Generic;

namespace AssociationsService.Entities.Association
{
    public class Association
    {
        public string Text { get; set; }

        public IEnumerable<AssociationItem> Items { get; set; }
    }
}