using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Static
{
    public class CustomLocalizer : GridLocalizer
    {
        public override  string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                case GridStringId.FindControlFindButton: return "Trova";
                case GridStringId.FindControlClearButton: return "Cancella";
                case GridStringId.SearchLookUpAddNewButton: return "Nuovo";
                default: return base.GetLocalizedString(id);
            }
        }
    }
}
