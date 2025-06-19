//Afisarea datelor citire din BD
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
