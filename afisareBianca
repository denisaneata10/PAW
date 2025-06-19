data grid view: cosCumparaturiDataGridView.AutoGenerateColumns = true;

BindingList<Produs> produse = CosCumparaturi.getProduse();

cosCumparaturiDataGridView.DataSource = produse;



list view:  bicicleteListView.Columns.Add("Cod Bicicleta");
 bicicleteListView.Columns.Add("Denumire statie");
 bicicleteListView.Columns.Add("Km parcursi");

 bicicleteListView.FullRowSelect = true;

 foreach(var b in biciclete){
     var item = new ListViewItem(b.codB.ToString());
     item.SubItems.Add(b.denumireStatie);
     item.SubItems.Add(b.kmParcursi.ToString());

     bicicleteListView.Items.Add(item);
 }
