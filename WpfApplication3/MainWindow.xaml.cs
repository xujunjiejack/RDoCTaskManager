using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private String rocket_configuration = "R:/master script/rocket data mapper and uploader/rocket/workspace_path_config.json";
        private Dictionary<String, String> workspace;
        private ObservableCollection<String> _mappingManagerList;

        private const string _coins2ndarKey = "Coins to Ndar";
        private const string _wtp2ndarKey = "WTP to Ndar";
        private const string _prompt = "Choose mapping manager";

        private  Dictionary<String, String> make_template_actions = new Dictionary<String, String>
        {
            [_coins2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\make_template.bat",
            [_wtp2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\make_template_wtp2ndar.bat"
        };

        private Dictionary<String, String> run_rocket_actions = new Dictionary<String, String>
        {
            [_coins2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\convert_template-2.bat",
            [_wtp2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\convert_template_wtp2ndar.bat"
        };

        private Dictionary<String, String> update_template_actions = new Dictionary<String, String>
        {
            [_coins2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\update_template_coins2ndar.bat",
            [_wtp2ndarKey] = "R:\\master script\\rocket data mapper and uploader\\update_template_wtp2ndar.bat"
        };

        public MainWindow()
        {
            InitializeComponent();
            Dictionary<String, Dictionary<String,String>> config = JsonConvert.DeserializeObject<Dictionary<String, Dictionary<String, String>>>(File.ReadAllText(this.rocket_configuration));
            this.workspace = config["workspace"];
            _mappingManagerList = new ObservableCollection<string>();
            _mappingManagerList.Add(_prompt);
            _mappingManagerList.Add(_coins2ndarKey);
            _mappingManagerList.Add(_wtp2ndarKey);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MappingManagerLists.ItemsSource = _mappingManagerList;
            MappingManagerLists.SelectedIndex = 0;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            String filePath = "R:\\DataDictionaryDifferences.xlsx";
            System.Diagnostics.Process.Start(filePath);
        }

        private void start_function(Dictionary<String, String> action_table)
        {
            string rocketStartPath = "";
            switch ((string)MappingManagerLists.SelectedItem)
            {
                case _prompt:
                    MessageBox.Show("Please choose a mapping manager");
                    return;

                case _coins2ndarKey:
                    rocketStartPath = action_table[_coins2ndarKey];
                    break;

                case _wtp2ndarKey:
                    rocketStartPath = action_table[_wtp2ndarKey];
                    break;

                default:
                    MessageBox.Show("Please choose a mapping manager");
                    return;

            }
            System.Diagnostics.Process.Start(rocketStartPath);
        }


        private void Convert_Template_Click(object sender, RoutedEventArgs e) => start_function(run_rocket_actions);

        private void Make_Template_Click(object sender, RoutedEventArgs e) => start_function(make_template_actions);

        private void Update_Template_Click(object sender, RoutedEventArgs e) => start_function(update_template_actions);

        private void Open_Ref_Click(object sender, RoutedEventArgs e)
        {
            String rocket_template = workspace["mapping_file_dir"];
            System.Diagnostics.Process.Start(rocket_template);
        }

        private void Rocket_Documentation(object sender, RoutedEventArgs e)
        {
            String rocketStartPath = "http://go.wisc.edu/rocket_readme";
            System.Diagnostics.Process.Start(rocketStartPath);
        }

        private void Run_Validation_Tool(object sender, RoutedEventArgs e)
        {
            String validationTool = "R:\\validationtool-client.jnlp";
            System.Diagnostics.Process.Start(validationTool);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
