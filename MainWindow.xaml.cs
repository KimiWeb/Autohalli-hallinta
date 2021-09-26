
    public partial class MainWindow : Window
    {
        // array-taulukko
        autoTiedot[] autonTiedot = new autoTiedot[50];
        struct autoTiedot
        {
            public string merkki;
            public string malli;
            public string rekisterinumero;
            public string valmistusvuosi;
            public string huollettu;
            public string kuvanNimi;
            public string hinta;
        }

        int autoInd = 0;
        public MainWindow()
        {
            InitializeComponent();
            // tämä tapahtuu ohjelman alkaessa
            // objekti nimeltä lukija luo yhteyden xml-tiedostoon (stream)

            XmlReader lukija = XmlReader.Create("XMLAutot.xml");
            string autonID = "";
            lukija.MoveToContent();
            // Toista tiedoston lukemista, niin kauan kuin tiedostossa on luettavaa
            while (lukija.Read())
            {
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "AUTO")
                {
                    if (lukija.HasAttributes)
                    {
                        autonID = lukija.GetAttribute("ID");
                        lstAutojenID.Items.Add(autonID);
                    }
                }

                // lisää autonn tietoja array-taulukkoon autonTiedot
                if (lukija.NodeType == XmlNodeType.Element
                        && lukija.Name == "MERKKI")
                {
                    lukija.Read();
                    autonTiedot[autoInd].merkki = lukija.Value;
                    // indeksi ei kasva, koska auto pysyy samana merkki yms tietojen etsinnässä
                }
                if (lukija.NodeType == XmlNodeType.Element
                      && lukija.Name == "MALLI")
                {
                    lukija.Read();
                    autonTiedot[autoInd].malli = lukija.Value;
                }
                if (lukija.NodeType == XmlNodeType.Element
                      && lukija.Name == "REKISTERINUMERO")
                {
                    lukija.Read();
                    autonTiedot[autoInd].rekisterinumero = lukija.Value;
                }
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "HUOLTAMATON")
                {
                    lukija.Read();
                    autonTiedot[autoInd].huollettu= lukija.Value;
                }
                if (lukija.NodeType == XmlNodeType.Element
                      && lukija.Name == "VALMISTUSVUOSI")
                {
                    lukija.Read();
                    autonTiedot[autoInd].valmistusvuosi = lukija.Value;
                }
                if (lukija.NodeType == XmlNodeType.Element
                    && lukija.Name == "PICTURE")
                {
                    lukija.Read();
                    autonTiedot[autoInd].kuvanNimi = lukija.Value;
                }
                if (lukija.NodeType == XmlNodeType.Element
                      && lukija.Name == "HINTA")
                {
                    lukija.Read();
                    autonTiedot[autoInd].hinta = lukija.Value;
                    autoInd++;
                }
            }
        }
        // tapahtuma-aliohjelma, joka suoritetaan aina, kun listalaatikon rivivalinta vaihtuu
        private void lstAutojenID_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int haettavanIndeksi = lstAutojenID.SelectedIndex;
            lblMerkki.Content = autonTiedot[haettavanIndeksi].merkki;
            lblMalli.Content = autonTiedot[haettavanIndeksi].malli;
            lblRekisterinumero.Content = autonTiedot[haettavanIndeksi].rekisterinumero;
            lblValmistusvuosi.Content = autonTiedot[haettavanIndeksi].valmistusvuosi;
            lblHinta.Content = autonTiedot[haettavanIndeksi].hinta;
            if (autonTiedot[haettavanIndeksi].huollettu == "yes")
            {
                rdbHuollettu.IsChecked = true;
            }
            if (autonTiedot[haettavanIndeksi].huollettu == "no")
            {
                rdbEiHuollettu.IsChecked = true;
            }
            string kuvaTiedostonPolkuJaNimi = @"C:\Users\KimiPC\Pictures\" + autonTiedot[haettavanIndeksi].kuvanNimi;
            ImageSource imageSource = new BitmapImage(new Uri(kuvaTiedostonPolkuJaNimi));
            ImgAuto.Source = imageSource;
        }
    }
}



