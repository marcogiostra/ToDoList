using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Static
{
    public class DEV_GridViewRowSearch
    {
        #region PULBLIC FUNCTION
        public static void ExecuteSearchAndExpand_ByString(GridView pGridView, BandedGridView pBandedGridView, string pColumnToSearch, string pKey)
        {
            if (pGridView != null)
            {
                var column = pGridView.Columns.FirstOrDefault(c => c.FieldName == pColumnToSearch);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(pGridView, null, (GridColumn)column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                var column = pBandedGridView.Columns.FirstOrDefault(c => c.FieldName == pColumnToSearch);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(null, pBandedGridView, (GridColumn)column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        public static void ExecuteSearchAndExpand_ByString(GridView pGridView, BandedGridView pBandedGridView, GridColumn pColumn, string pKey)
        {
            if (pGridView != null)
            {
                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(pGridView, null, pColumn, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(null, pBandedGridView, pColumn, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        public static void ExecuteSearchAndExpandColumnFileName_ByString(GridView pGridView, BandedGridView pBandedGridView, string pFileName, string pKey)
        {
            if (pGridView != null)
            {
                GridColumn column = pGridView.Columns.ColumnByFieldName(pFileName);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(pGridView, null, column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                GridColumn column = pBandedGridView.Columns.ColumnByFieldName(pFileName);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byString(null, pBandedGridView, column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        //
        public static void ExecuteSearchAndExpand_ByInt(GridView pGridView, BandedGridView pBandedGridView, string pColumnToSearch, int pKey)
        {
            if (pGridView != null)
            {
                var column = pGridView.Columns.FirstOrDefault(c => c.FieldName == pColumnToSearch);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(pGridView, null, (GridColumn)column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                var column = pBandedGridView.Columns.FirstOrDefault(c => c.FieldName == pColumnToSearch);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(null, pBandedGridView, (GridColumn)column, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        public static void ExecuteSearchAndExpand_ByInt(GridView pGridView, BandedGridView pBandedGridView, GridColumn pColumn, int pKey)
        {
            if (pGridView != null)
            {
                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(pGridView, null, pColumn, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(null, pBandedGridView, pColumn, pKey);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        public static void ExecuteSearchAndExpandColumnFileName_ByInt(GridView pGridView, BandedGridView pBandedGridView, string pFileName, int pSearchString)
        {
            if (pGridView != null)
            {
                GridColumn column = pGridView.Columns.ColumnByFieldName(pFileName);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(pGridView, null, column, pSearchString);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(pGridView, null, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pGridView.MakeRowVisible(foundRowHandle, true);
                    pGridView.FocusedRowHandle = foundRowHandle;
                    pGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
            else
            {
                GridColumn column = pBandedGridView.Columns.ColumnByFieldName(pFileName);

                // Cerca la riga contenente il valore nella colonna specificata
                int foundRowHandle = FindRowHandleByColumnValue_byInt(null, pBandedGridView, column, pSearchString);

                if (foundRowHandle != GridControl.InvalidRowHandle)
                {
                    // Chiudi tutti i raggruppamenti
                    pBandedGridView.CollapseAllGroups();

                    // Apri solo i raggruppamenti fino al quarto livello per la riga trovata
                    ExpandGroupForRow(null, pBandedGridView, foundRowHandle);

                    // Assicurati che la riga sia visibile e selezionala
                    pBandedGridView.MakeRowVisible(foundRowHandle, true);
                    pBandedGridView.FocusedRowHandle = foundRowHandle;
                    pBandedGridView.SelectRow(foundRowHandle);
                }
                else
                {
                    // Nessuna riga trovata, puoi gestire il caso qui, ad esempio con un messaggio
                    MessageBox.Show("Riga non trovata!");
                }
            }
        }
        #endregion PULBLIC FUNCTION

        #region f()_PRIVATE

        private static int FindRowHandleByColumnValue_byString(GridView pGridView, BandedGridView pBandedGridView, GridColumn pColumn, string value)
        {
            if (pGridView != null)
            {
                if (pColumn != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pGridView.DataRowCount; i++)
                    {
                        string cellValue = pGridView.GetRowCellValue(i, pColumn).ToString();

                        if (cellValue == value)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga

            }
            else
            {
                if (pColumn != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pBandedGridView.DataRowCount; i++)
                    {
                        string cellValue = pBandedGridView.GetRowCellValue(i, pColumn).ToString();

                        if (cellValue == value)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga

            }


        }
        private static int FindRowHandleByColumnValue_byInt(GridView pGridView, BandedGridView pBandedGridView, GridColumn pColumn, int value)
        {
            if (pGridView != null)
            {
                if (pColumn != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pGridView.DataRowCount; i++)
                    {
                        int cellValue = Convert.ToInt32(pGridView.GetRowCellValue(i, pColumn));

                        if (cellValue == value)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga


            }
            else
            {
                if (pColumn != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pBandedGridView.DataRowCount; i++)
                    {
                        int cellValue = Convert.ToInt32(pBandedGridView.GetRowCellValue(i, pColumn));

                        if (cellValue == value)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga

            }


        }

        private static int FindRowHandleByColumnValue_byStringString(GridView pGridView, BandedGridView pBandedGridView, GridColumn pColumn1, string value1, GridColumn pColumn2, string value2)
        {
            if (pGridView != null)
            {
                if (pColumn1 != null && pColumn2 != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pGridView.DataRowCount; i++)
                    {
                        string cellValue1 = pGridView.GetRowCellValue(i, pColumn1).ToString();
                        string cellValue2 = pGridView.GetRowCellValue(i, pColumn2).ToString();

                        if (cellValue1 == value1 && cellValue2 == value2)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga

            }
            else
            {
                if (pColumn1 != null && pColumn2 != null)
                {
                    // Scansiona tutte le righe del GridView
                    for (int i = 0; i < pBandedGridView.DataRowCount; i++)
                    {
                        string cellValue1 = pBandedGridView.GetRowCellValue(i, pColumn1).ToString();
                        string cellValue2 = pBandedGridView.GetRowCellValue(i, pColumn2).ToString();

                        if (cellValue1 == value1 && cellValue2 == value2)
                        {
                            return i; // Restituisce l'handle della riga trovata
                        }
                    }
                }

                return GridControl.InvalidRowHandle; // Restituisce un valore non valido se non trova la riga

            }


        }

        private static void ExpandGroupForRow(GridView pGridView, BandedGridView pBandedGridView, int rowHandle)
        {
            if (pGridView != null)
            {
                // Ottieni tutti gli handle di raggruppamento per la riga trovata
                int parentGroup = pGridView.GetParentRowHandle(rowHandle);

                // Espandi tutti i gruppi fino al quarto livello
                while (pGridView.IsGroupRow(parentGroup))
                {
                    pGridView.ExpandGroupRow(parentGroup);
                    parentGroup = pGridView.GetParentRowHandle(parentGroup);
                }
            }
            else
            {
                // Ottieni tutti gli handle di raggruppamento per la riga trovata
                int parentGroup = pBandedGridView.GetParentRowHandle(rowHandle);

                // Espandi tutti i gruppi fino al quarto livello
                while (pBandedGridView.IsGroupRow(parentGroup))
                {
                    pBandedGridView.ExpandGroupRow(parentGroup);
                    parentGroup = pBandedGridView.GetParentRowHandle(parentGroup);
                }
            }
        }
        #endregion f()_PRIVATE
    }
}
