using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Jumbalaya.Core;

namespace JumbalayaGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<BoardRow> rows;
        private ObservableCollection<string> moveOptions;
        private ObservableCollection<string> jumbalayaOptions;
        private IWordIndex wordIndex;
        private IJumbalayaFinder jumbalayaFinder;
        private TileTray tray;
        private const string wordListPath = @"..\..\..\Resources\wordsEn_filtered.txt";

        public MainWindow()
        {
            InitializeComponent();
            jumbalayaFinder = new SimpleJumabalayaFinder(wordListPath);
            wordIndex = new SlightlyLessFancyWordIndex();
            WordIndexFactory.FillWordIndex(wordIndex, wordListPath);
            rows = new ObservableCollection<BoardRow>
            {
                new BoardRow {Text = "sun"},
                new BoardRow {Text = "perfectly"},
                new BoardRow {Text = "aquifer"},
                new BoardRow {Text = "climb"},
                new BoardRow {Text = "thank"},
                new BoardRow {Text = "joyous"},
                new BoardRow {Text = "extra"},
                new BoardRow {Text = "changed"},
                new BoardRow {Text = "wise"},
            };
            RowListBox.ItemsSource = rows;

            moveOptions = new ObservableCollection<string>();
            OptionListBox.ItemsSource = moveOptions;

            this.tray = new TileTray
            {
                Text = "traytext"
            };
            TileTrayTextBox.DataContext = tray;

            RowListBox.SelectedIndex = 0;
        }

        private void RowListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateMoves();
        }

        private void Row_OnGotFocus(object sender, RoutedEventArgs e)
        {
            BoardRow row = (BoardRow)((FrameworkElement)sender).DataContext;
            RowListBox.SelectedItem = row;
        }

        private void UpdateOptions()
        {
            var selectedRow = this.SelectedRow;
            if (selectedRow != null)
            {
                var newOptions = wordIndex.FindAvailableMoves(selectedRow.Text, tray);
                moveOptions = new ObservableCollection<string>(newOptions.OrderByDescending(op => op.Length));
                OptionListBox.ItemsSource = moveOptions;
                //foreach (var newOption in newOptions)
                //{
                //    moveOptions.Add(newOption);
                //}
                //moveOptions = moveOptions.OrderByDescending(op => op.Length);
            }
            OptionCountTextBlock.Text = string.Format("Available Moves ({0})", moveOptions.Count);
        }

        private BoardRow SelectedRow
        {
            get
            {
                BoardRow selectedRow = (BoardRow)RowListBox.SelectedItem;
                return selectedRow;
            }
        }

        private void UpdateMoves()
        {
            this.UpdateOptions();
            this.UpdateJumbalayas();
        }

        private void UpdateJumbalayas()
        {
            BoardRow selectedRow = this.SelectedRow;
            if (selectedRow != null)
            {
                Board board = new Board
                {
                    Words = this.rows.Select(row => row.Text).ToArray()
                };

                List<string> jumbalayas = jumbalayaFinder.FindJumbalayas(board);
                this.jumbalayaOptions = new ObservableCollection<string>(jumbalayas.OrderByDescending(j => j.Length));
                this.JumbalayaListBox.ItemsSource = this.jumbalayaOptions;
                this.AvailableJumbalayasTextBlock.Text = string.Format("Available Jumbalayas ({0})", jumbalayas.Count);
            }
        }

        private void TileTrayTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateMoves();
        }

        private void RowTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateMoves();
        }
    }
}
