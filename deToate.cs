Clase:
  public class Angajat
  {
      private string _nume;
      private int _oreLucrate;
      private float _salariuOrar;
      public const string _constanta = "A";
      private static int _nrAngajati = 0;
      private string _cod;

      public Angajat(string nume, int oreLucrate, float salariuOrar)
      {
          _nume = nume;
          _oreLucrate = oreLucrate;
          _salariuOrar = salariuOrar;

          _nrAngajati++;
          _cod = $"{_constanta}{_nrAngajati}";
      }

      public string Nume
      {
          get=> _nume;
          set => _nume = value;

      }

      public int OreLucrate
      {
          get => _oreLucrate;
          set => _oreLucrate = value;

      }

      public float SalariuOrar
      {
          get => _salariuOrar;
          set => _salariuOrar = value;
      }

      public string Cod
      {
          get => _cod;

      }

      public override string ToString()
      {
          return $" Cod: {_cod}, Nume: {_nume}, Ore lucrate: {_oreLucrate}, Salariu orar: {_salariuOrar}";
      }
  }

Citire din baza de date:

namespace SubiectTutoring
{
    public class AngajatRepository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Denisa\\Documents\\tutoring.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public List<Angajat> GetAngajat()
        {
            var list = new List<Angajat>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT Cod, Nume, Salariu, CodManager FROM Angajati", connection))
                {
                    var reader = command.ExecuteReader();
                    var angajat = new Angajat();
                    while (reader.Read())
                    {
                        angajat.Cod = reader.GetInt32(reader.GetOrdinal("Cod"));
                        angajat.Nume = reader.GetString(reader.GetOrdinal("Nume"));
                        angajat.Salariu = reader.GetDouble(reader.GetOrdinal("Salariu"));

                        int index = reader.GetOrdinal("CodManager");

                        if (!reader.IsDBNull(index))
                        {
                            angajat.CodManager = reader.GetInt32(index);
                        }
                        list.Add(angajat);
                    }

                }
                return list;
            }

        }
    }
}

//Afisarea datelor citite din BD
//-cream noi coloanele pt DataGridView

public partial class Form1: Form
{
    private Repository _repository = new Repository();
    public Form1()
    {
        InitializeComponent();
        AddTextBoxColumn("Marca",true);
        AddTextBoxColumn("Nume");
        AddTextBoxColumn("Salariu");
        AddButtonColumn("Premiaza");
        AddCheckBoxColumn("AreDoctorat");
        profesorsDataGridView.DataSource = _repository.GetProfesors();
        //profesorsDataGridView.ContextMenuStrip = contextMenuStrip1;
        this.ContextMenuStrip = contextMenuStrip1;
    }
    private void AddTextBoxColumn(string propertyName,bool isReadonly=false)
    {
        var column = new DataGridViewTextBoxColumn();
        column.Name = propertyName;
        column.HeaderText = propertyName;
        column.DataPropertyName = propertyName;//stie cu ce data se leaga?, aici trb sa fie exact numele din clasa, gen Marca de ex
        column.ReadOnly = isReadonly;
        profesorsDataGridView.Columns.Add(column);
        KeyPreview = true;
    }
    private void AddButtonColumn(string columnName)
    {
        var column = new DataGridViewButtonColumn();
        column.Name = columnName;
        column.Text = columnName;
        column.UseColumnTextForButtonValue = true;
        profesorsDataGridView.Columns.Add(column);

    }
private void AddCheckBoxColumn(string propertyName, bool isReadonly = false)
{
    var column = new DataGridViewCheckBoxColumn();
    column.Name = propertyName;
    column.HeaderText = propertyName;
    column.DataPropertyName = propertyName; // trebuie să fie exact numele proprietății din clasa Profesor
    column.ReadOnly = isReadonly;
    profesorsDataGridView.Columns.Add(column);
}
//-cu autogenerare de coloane si adaugare obiecte din cod

namespace SubiectTraseuPAW
{
    public partial class Form1 : Form
    {
        private Traseu traseu;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            traseu = new Traseu();

            // Adaugă două locații din cod
            traseu.AdaugaLocatie(new Locatie(44.43m, 26.10m, "Bucuresti"));
            traseu.AdaugaLocatie(new Locatie(45.75m, 21.23m, "Timisoara"));

            // Afișează locațiile în grilă
            AfiseazaLocatiiInGrid();

            // Actualizează bara de stare
            toolStripStatusLabel1.Text = $"Lungime traseu: {traseu.LungimeTraseu:F2} km";
        }

        private void AfiseazaLocatiiInGrid()
        {
            // Resetăm coloanele o singură dată
            if (dataGridViewLocatii.Columns.Count == 0)
            {
                dataGridViewLocatii.Columns.Add("Denumire", "Denumire");
                dataGridViewLocatii.Columns.Add("Latitudine", "Latitudine");
                dataGridViewLocatii.Columns.Add("Longitudine", "Longitudine");
            }

            // Ștergem datele vechi
            dataGridViewLocatii.Rows.Clear();

            // Adăugăm rânduri pentru fiecare locație
            foreach (var loc in traseu.GetLocatii())
            {
                dataGridViewLocatii.Rows.Add(loc.Denumire, loc.Latitudine, loc.Longitudine);
            }
        }
    }
}
//ca sa nu se poata edita:
profesorsDataGridView.ReadOnly = true;
Bianca:
data grid view: 
cosCumparaturiDataGridView.AutoGenerateColumns = true;

BindingList<Produs> produse = CosCumparaturi.getProduse();

cosCumparaturiDataGridView.DataSource = produse;


list view: 
 bicicleteListView.Columns.Add("Cod Bicicleta");
 bicicleteListView.Columns.Add("Denumire statie");
 bicicleteListView.Columns.Add("Km parcursi");

 bicicleteListView.FullRowSelect = true;

 foreach(var b in biciclete){
     var item = new ListViewItem(b.codB.ToString());
     item.SubItems.Add(b.denumireStatie);
     item.SubItems.Add(b.kmParcursi.ToString());

     bicicleteListView.Items.Add(item);
 }

Apasare taste actiuni:
   private void profesorsDataGridView_KeyDown(object sender, KeyEventArgs e)
   {
       if(e.Alt && e.KeyCode == Keys.E)
       {
           Application.Exit();
       }
   }


Salvare si stergere din baza de date:
public void Salveaza(Angajat angajat)
 {
     using(var connection=new SqlConnection(_connectionString))
     {
         connection.Open();
         using (var command = new SqlCommand("INSERT INTO Angajati (Nume,Salariu,CodManager) Values (@Nume, @Salariu,@CodManager)", connection))
         {
             //@ ->parametrii
             command.Parameters.AddWithValue("Nume", angajat.Nume);
             command.Parameters.AddWithValue("Salariu", angajat.Salariu);
             command.Parameters.AddWithValue("CodManager", angajat.CodManager);

             command.ExecuteNonQuery();
         }
                    
     }
 }
 public void Sterge(Angajat angajat)
 {
     using (var connection = new SqlConnection(_connectionString))
     {
         connection.Open();
         using (var command = new SqlCommand("DELETE FROM Angajati where Cod=@Cod", connection))
         {
                 
             command.Parameters.AddWithValue("Cod", angajat.Cod);

             command.ExecuteNonQuery();
         }

     }
 }
Buton de salvare din formularul de adaugare:
private void saveButton_Click(object sender, EventArgs e)
{
    var angajat = new Angajat();
    angajat.Nume = numeTextBox.Text;
    angajat.Salariu =(double) salariuUpDown.Value;
    angajat.CodManager =(int) managerComboBox.SelectedValue;

    _angajatRepository.Salveaza(angajat);
    Close();
}

SAU:
private void SalveazaButton_Click(object sender, EventArgs e)
{
    // Validare simplă
    if (string.IsNullOrWhiteSpace(marcaTextBox.Text) ||
        string.IsNullOrWhiteSpace(numeTextBox.Text))
    {
        MessageBox.Show("Toate câmpurile text trebuie completate.");
        return;
    }

    // Creare profesor nou
    var profesor = new Profesor()
    {
        Marca = marcaTextBox.Text,
        Nume = numeTextBox.Text,
        Salariu = (double)salariuNumericUpDown.Value,
        AreDoctorat = doctoratCheckBox.Checked
    };

    _repository.AddProfesor(profesor); // adaugă în listă sau DB

    // Actualizează DataGridView
    profesorsDataGridView.DataSource = null;
    profesorsDataGridView.DataSource = _repository.GetProfesors();

    // Resetare formular
    marcaTextBox.Text = "";
    numeTextBox.Text = "";
    salariuNumericUpDown.Value = 0;
    doctoratCheckBox.Checked = false;

    MessageBox.Show("Profesor adăugat cu succes!");
}

Subiect Profesori
Form:
namespace PregatirePaw_6_iunie
{
    public partial class Form1: Form
    {
        private Repository _repository = new Repository();
        public Form1()
        {
            InitializeComponent();
            AddTextBoxColumn("Marca",true);
            AddTextBoxColumn("Nume");
            AddTextBoxColumn("Salariu");
            AddButtonColumn("Premiaza");
            profesorsDataGridView.DataSource = _repository.GetProfesors();
            //profesorsDataGridView.ContextMenuStrip = contextMenuStrip1;
            this.ContextMenuStrip = contextMenuStrip1;
        }
        private void AddTextBoxColumn(string propertyName,bool isReadonly=false)
        {
            var column = new DataGridViewTextBoxColumn();
            column.Name = propertyName;
            column.HeaderText = propertyName;
            column.DataPropertyName = propertyName;//stie cu ce data se leaga?, aici trb sa fie exact numele din clasa, gen Marca de ex
            column.ReadOnly = isReadonly;
            profesorsDataGridView.Columns.Add(column);
            KeyPreview = true;
        }
        private void AddButtonColumn(string columnName)
        {
            var column = new DataGridViewButtonColumn();
            column.Name = columnName;
            column.Text = columnName;
            column.UseColumnTextForButtonValue = true;
            profesorsDataGridView.Columns.Add(column);

        }

        private void profesorsDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;//sau (DatagridView)sender
            var profesor = grid.Rows[e.RowIndex].DataBoundItem as Profesor;//iau randul pt care se face editarea
            //iau elementul care e elegat la aia
            _repository.UpdateProfesor(profesor);
            MessageBox.Show("Profesul a fost modificat cu succes!");
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var document = new PrintDocument();
            //evt e evtargs cv gen
            document.PrintPage += (source, evt) =>
            {
                var profesors = _repository.GetProfesors();
                var positionY = 50;
                foreach (var profesor in profesors)
                {
                    evt.Graphics.DrawString(profesor.ToString(), new Font("Arial", 10), Brushes.Black, new Point(50, positionY));//50 e valoarea lui x, const 50 fata de margine
                    positionY += 20;
                }
            };
            var print = new PrintPreviewDialog()
            {
                Document = document,
                Width=600,
                Height=900
            };
            print.ShowDialog(); ;
        }

        private void totalButton_Click(object sender, EventArgs e)
        {
            var profesors = _repository.GetProfesors();
            double sum = 0;
            foreach(var prof in profesors)
            {
                sum += prof + 0;//adc prof+0 imi va da salariul ca am op +
            }
            MessageBox.Show($"Suma salariilor este {sum}");
        }

        private void profesorsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;

            var columnName = grid.Columns[e.ColumnIndex].Name;//ma uit pe ce coloana am fct click si am luat numele ei
            if (columnName == "Premiaza")
            {
                var profesor = grid.Rows[e.RowIndex].DataBoundItem as Profesor;//iau profesorul
                profesor.Premiaza();
                _repository.UpdateProfesor(profesor);

                profesorsDataGridView.DataSource = null;//dc il pun pe null??
                profesorsDataGridView.DataSource = _repository.GetProfesors();
            }
        }

        private void profesorsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Alt && e.KeyCode == Keys.E)
            {
                Application.Exit();
            }
        }

        private void serializeButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML files|*.xml";//asta se vede acl la type
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using(var stream=new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                    var serializer = new XmlSerializer(typeof(List<Profesor>));
                    serializer.Serialize(stream, _repository.GetProfesors());
                }
            }
        }
    }
}
Repository:
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregatirePaw_6_iunie
{
    public class Repository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\rucsa\\OneDrive\\Documents\\pregatireTest.mdf;Integrated Security=True;Connect Timeout=30";
        //adresa serverului 
        public List<Profesor> GetProfesors()
        {
            var list = new List<Profesor>();
            //get from db
            using (var connection = new SqlConnection(_connectionString))
            {
                //mai elegant
                connection.Open();
                using (var command = new SqlCommand("Select Marca,nume,salariu FROM tblProfesor", connection))
                {
                    var reader = command.ExecuteReader();//utilizat cand comanda returneaza niste date->tabele
                    //nonquery -> pt update etc
                    //scalar ->  returneaza o valoare? n am inteles
                    while (reader.Read())
                    {
                        var marca = reader.GetInt32(0);//primul int / pot face si (int)reader["Marca"] sau reader.getInt32(reader.getOrdinal("Marca"));
                        //cea mai curata varianta e ultima
                        var nume = reader.GetString(reader.GetOrdinal("Nume"));
                        var salariu = reader.GetDouble(reader.GetOrdinal("Salariu"));//asta e aia curata gen
                        list.Add(new Profesor(marca, nume, salariu));

                    }
                }
                connection.Close();//nu e obligatoriu ca am IDisposable oricum
            }
            //connection.Dispose();//fct manual
            return list;
        }
        public void UpdateProfesor(Profesor profesor)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand($"Update tblProfesor SET Nume=@Nume, Salariu=@Salariu WHERE Marca=@Marca",connection))
                {
                    //asta cu @ e parametrizare
                    command.Parameters.AddWithValue("Nume", profesor.Nume);
                    command.Parameters.AddWithValue("Salariu", profesor.Salariu);
                    command.Parameters.AddWithValue("Marca", profesor.Marca);
                    //puteam si command.Connection=connection
                    command.ExecuteNonQuery();//nu astept nmc din partea lui
                }
                connection.Close();
            }
        }
    }
}
Profesor:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregatirePaw_6_iunie
{
    [Serializable]
    public class Profesor: IPremiere
    {
        public int Marca { get; set; }//atribut=marca, proprietate=get ul?
        public string Nume { get; set; }
        public double Salariu { get; set; }
        public Profesor(int marca,string nume,double salariu)
        {
            this.Marca = marca;
            this.Nume = nume;
            Salariu = salariu;
        }
        public override string ToString()
        {
            return $"{Marca} {Nume} {Salariu}";
        }
        public Profesor()
        {
            
        }
        public void Premiaza()
        {
            Salariu = Salariu * 1.3;
        }

        //mereu static la supraincarcare
        public static double operator +(double left, Profesor right)
        {//asta e cerinta
            return left + right.Salariu;
        }
        public static double operator +(Profesor left, double right)
        {//asa de fun
            return left.Salariu + right;
        }
    }
}
Subiect Tutoring:

AngajatRepository:
namespace Subiect1
{
   public  class AngajatRepository
    {
        private string _connectionString = "----";
        public List<Angajat> GetAngajati()
        {
            var list = new List<Angajat>();

            using(var connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Cod, Nume, Salariu, CodManager from Angajati ", connection))//nu *, e mai bine sa scriem coloanele pt a avea control
                {
                    //command.ExecuteNonQuery();// folosim asta cand comanda nu ne intoarce nimic (insert, update, delete)
                    //scalar->returneaza date-> cand e apel de functii de exemplu, returneaza o valoare, un sg rand+ o sg coloana ->ex suma sau cv
                    //reader->returneaza date, returneaza tabele/linii
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var angajat = new Angajat();
                        angajat.Cod = reader.GetInt32(reader.GetOrdinal("Cod"));//get ordinal= indexul coloanei cu acel nume
                        angajat.Nume = reader.GetString(reader.GetOrdinal("Nume"));
                        angajat.Salariu = reader.GetDouble(reader.GetOrdinal("Salariu"));

                        int index = reader.GetOrdinal("CodManager");

                        if (!reader.IsDBNull(index))
                        {
                            //e null in codManager
                            angajat.CodManager = reader.GetInt32(index);
                        }
                        list.Add(angajat);
                    }
                }
            }


            return list;
        }

        public void Salveaza(Angajat angajat)
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Angajati (Nume,Salariu,CodManager) Values (@Nume, @Salariu,@CodManager)", connection))
                {
                    //@ ->parametrii
                    command.Parameters.AddWithValue("Nume", angajat.Nume);
                    command.Parameters.AddWithValue("Salariu", angajat.Salariu);
                    command.Parameters.AddWithValue("CodManager", angajat.CodManager);

                    command.ExecuteNonQuery();
                }
                    
            }
        }
        public void Sterge(Angajat angajat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Angajati where Cod=@Cod", connection))
                {
                 
                    command.Parameters.AddWithValue("Cod", angajat.Cod);

                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
Form1.cs

namespace Subiect1
{//partial->aceeasi clasa poate fi impartita in mai multe fisiere
    public partial class Form1: Form
    {
        private AngajatRepository _angajati_repository = new AngajatRepository();
        private List<Angajat> _angajati;

        private int _currentY = 40;
        public Form1()
        {
            InitializeComponent();
            LoadInitialData();//daca e asa nu o sa avem id autogenerat
            BuildTree();

            angajatiListView.View = View.Details;
            //list view cu details->obligatoriu setam coloane
            angajatiListView.Columns.Add("Nume",150);
            angajatiListView.Columns.Add("Salariu",80);
            angajatiListView.Columns.Add("Manager",150);

           
            RegisterKeyEvents();
        }

        private void LoadInitialData()
        {
            using (var stream = new FileStream("date.csv", FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var parts = line.Split(',');
                        var angajat = new Angajat();
                        angajat.Nume = parts[0];
                        angajat.Salariu = Convert.ToDouble(parts[1]);
                        if (!string.IsNullOrEmpty(parts[2]))
                        {
                            angajat.CodManager = Convert.ToInt32(parts[2]);
                        }
                        _angajati_repository.Salveaza(angajat);
                      

                    }
                 

                }
            }
        }

        private void RegisterKeyEvents()
        {
            angajatiListView.KeyUp += (s, e) =>
            {
                var listView = s as ListView;
                if (e.KeyCode == Keys.Delete)
                {
                    if (listView.SelectedItems.Count > 0)
                    {
                        foreach(var item in listView.SelectedItems)
                        {
                            var listViewItem = item as ListViewItem;
                            var angajat = listViewItem.Tag as Angajat;
                            if (_angajati.Any(x => x.CodManager == angajat.Cod))
                            {
                                //nu putem sterge pentru ca e managerul cuiv
                                MessageBox.Show($"Nu se poate sterge angajatul {angajat.Nume} deoarece este manager.", "Eroare",MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                            else
                            {
                                _angajati_repository.Sterge(angajat);
                                BuildTree();
                                angajatiListView.Items.Clear();
                            }
                        }
                    }
                }
                if (e.KeyCode == Keys.F6)
                {
                    angajatiListView.Items.Clear();
                    foreach(var angajat in _angajati.OrderByDescending(x => x.Salariu))
                    {
                        var item = new ListViewItem(angajat.Nume);
                        item.SubItems.Add(angajat.Salariu.ToString());
                        var manager = _angajati.FirstOrDefault(x => x.Cod == angajat.CodManager);//daca nu imi gaseste manager intoarce null
                        if (manager != null) {
                            item.SubItems.Add(manager.Nume);
                        }
                      
                        item.Tag = angajat;
                        angajatiListView.Items.Add(item);

                    }
                }
            };
        }

        private void BuildTree()
        {
            angajatiTreeView.Nodes.Clear();
            _angajati = _angajati_repository.GetAngajati();
            var manager = _angajati.First(x => x.CodManager == null);//cautam angajatul cu codul manager=0, ex CEO-ul, LINQ

            var node = new TreeNode(manager.Nume);
            node.Tag = manager;// informatii suplimentare, in caz ca avem nevoie mai tarziu

            angajatiTreeView.Nodes.Add(node);
            BuildTree(manager, node);

        }
        private void BuildTree(Angajat manager, TreeNode root)//overloading
        {
            var angajati = _angajati.Where(x => x.CodManager == manager.Cod);
            foreach(var angajat in angajati)
            {
                var node = new TreeNode(angajat.Nume);
                node.Tag = angajat;
                root.Nodes.Add(node);
                BuildTree(angajat, node);
            }
        }

        private void angajatiTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //dupa selectarea unui element din tree view
            angajatiListView.Items.Clear();
            var manager = e.Node.Tag as Angajat;//convertim la Angajat, echivalent cu (Angajat) inainte 

            var angajatiInSubordine = _angajati.Where(x => x.CodManager == manager.Cod);
            
            foreach(var angajat in angajatiInSubordine)
            {
                var item = new ListViewItem(angajat.Nume);
                item.SubItems.Add(angajat.Salariu.ToString());
                item.SubItems.Add(manager.Nume);
                item.Tag = angajat;
                angajatiListView.Items.Add(item);
            }
        }

    

        private void binarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Binary files|*.bin";
            if (dialog.ShowDialog()== DialogResult.OK)
            {
                using(var stream =new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, _angajati);
                }
            }
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML files|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                    var serializer = new XmlSerializer(typeof(List<Angajat>));
                    serializer.Serialize(stream, _angajati);
                }
            }
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "CSV files|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                   using(var writer=new StreamWriter(stream))
                    {
                        foreach(var angajat in _angajati)
                        {
                            writer.WriteLine(angajat.ToString());
                        }
                    }
                }
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var document = new PrintDocument();
            document.PrintPage += (s, ev) =>
            {
                ev.Graphics.DrawString("Nume angajat", new Font("Verdana", 10, FontStyle.Bold), Brushes.Black, new PointF(20, 20));//coltul de sus al paginii e 0 0?
                ev.Graphics.DrawString("Salariu", new Font("Verdana", 10, FontStyle.Bold), Brushes.Black, new PointF(400, 20));
                ev.Graphics.DrawString("Nume manager", new Font("Verdana", 10, FontStyle.Bold), Brushes.Black, new PointF(640, 20));
                var manager = _angajati.First(x => x.CodManager == null);

                PrintAngajatCurent(ev, manager,0);


                ev.HasMorePages = false;

            };
            var dialog = new PrintPreviewDialog()
            {
                Document = document, 
                Width=800,
                Height=1200
            };
            dialog.ShowDialog();

           //atasam o functie noua, fie asa cu functie anonima (s,ev)=>{} fie functie declarata dupa
        }

        private void PrintAngajatCurent(PrintPageEventArgs ev,Angajat manager,int x)
        {
            ev.Graphics.DrawString(manager.Nume, new Font("Verdana", 10), Brushes.Black,new PointF(x,_currentY));
            ev.Graphics.DrawString(manager.Salariu.ToString(), new Font("Verdana", 10), Brushes.Black, new PointF(400, _currentY));
            if (manager.CodManager != null){
                var managerulManagerului = _angajati.First(a => a.Cod == manager.Cod);
                ev.Graphics.DrawString(managerulManagerului.Nume, new Font("Verdana", 10), Brushes.Black, new PointF(640, _currentY));
            }

            _currentY += 20;
            var angajatiInSubOrdine = _angajati.Where(a => a.CodManager == manager.Cod);
            foreach(var angajat in angajatiInSubOrdine)
            {
                PrintAngajatCurent(ev, angajat, x + 20);
                
            }
            /*  a1
             *      a2
             *          a3
             *      a4 
             *      x variaza in functie de nivelul de recursivitate
             *      y doar creste 
             */
        }

        private void adaugaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AdaugaAngajatForm();
            form.ShowDialog();
            BuildTree();
        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ChartForm();
            form.ShowDialog();
        }

        private void adaugaAngajatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
Form de adaugare angajat

namespace Subiect1
{
    public partial class AdaugaAngajatForm: Form
    {
        private AngajatRepository _angajatRepository = new AngajatRepository();
        public AdaugaAngajatForm()
        {
            InitializeComponent();

            var angajati = _angajatRepository.GetAngajati().OrderByDescending(x=>x.Nume).ToList();
            managerComboBox.DataSource = angajati;

            managerComboBox.DisplayMember = "Nume";//vad numele
            managerComboBox.ValueMember = "Cod";//se selecteaza codul

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var angajat = new Angajat();
            angajat.Nume = numeTextBox.Text;
            angajat.Salariu =(double) salariuUpDown.Value;
            angajat.CodManager =(int) managerComboBox.SelectedValue;

            _angajatRepository.Salveaza(angajat);
            Close();
        }
    }
}

